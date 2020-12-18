using NAudio.Wave;
using RomanPort.LibSDR.Demodulators;
using RomanPort.LibSDR.Extras.RDS;
using RomanPort.LibSDR.Framework;
using RomanPort.LibSDR.Framework.Components.FFT.Processors;
using RomanPort.LibSDR.Framework.Extras;
using RomanPort.LibSDR.Framework.Resamplers.Decimators;
using RomanPort.LibSDR.Framework.Util;
using RomanPort.LibSDR.Sources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RomanPort.IQFileLabelingTool
{
    public class RadioPlayer
    {
        public readonly RadioFile file;
        public readonly RadioBuffers buffers;

        public bool fftReady;
        public FFTProcessorComplex mainFft;
        public FFTProcessorHalfFloat mpxFft;
        public RDSClient rds;

        public float lengthSeconds { get => source.GetLengthSeconds(); }
        public float positionSeconds { get => source.GetPositionSeconds(); }

        private Thread worker;
        private volatile WavStreamSource source;
        private volatile RadioState state;

        public const float DEMOD_BW = 250000;
        public const int OUTPUT_SAMPLERATE = 48000;

        public RadioPlayer(RadioFile file, RadioBuffers buffers)
        {
            this.file = file;
            this.buffers = buffers;
            mainFft = new FFTProcessorComplex(MainLabelingForm.FFT_SIZE);
        }

        public void Start()
        {
            if (state != RadioState.STOPPED)
                return;
            state = RadioState.STARTING;
            worker = new Thread(Worker);
            worker.IsBackground = true;
            worker.Start();
            while (state == RadioState.STARTING) ;
        }

        public void Seek(float posSeconds)
        {
            source.SafeSkipToSeconds(posSeconds);
        }

        public void Stop()
        {
            if (state == RadioState.RUNNING)
                state = RadioState.STOPPING;
            while (state != RadioState.STOPPED) ;
        }

        private unsafe void Worker()
        {
            //Open file and source
            FileStream fs = new FileStream(file.path, FileMode.Open, FileAccess.Read);
            source = new WavStreamSource(fs);
            float sampleRate = source.Open(buffers.bufferSize);

            //Open IQ decimator & filter
            int decimationRate = SdrFloatDecimator.CalculateDecimationRate(sampleRate, DEMOD_BW, out float actualSampleRate);
            SdrComplexDecimator decimator = new SdrComplexDecimator(decimationRate);

            //Open filter
            //var coefficients = FilterBuilder.MakeLowPassKernel(sampleRate, 650, (int)(DEMOD_BW / 2), WindowType.BlackmanHarris4);
            //IQFirFilter filter = new IQFirFilter(coefficients);

            //Open demodulator
            int audioDecimationRate = SdrFloatDecimator.CalculateDecimationRate(actualSampleRate, OUTPUT_SAMPLERATE, out float actualAudioSampleRate);
            WbFmDemodulator demod = new WbFmDemodulator(audioDecimationRate);
            mpxFft = demod.EnableMpxFFT(1024);
            demod.OnAttached(buffers.bufferSize);
            demod.OnInputSampleRateChanged(actualSampleRate);
            rds = demod.UseRds();

            //Open audio resamplers
            FloatArbResampler audioResamplerA = new FloatArbResampler(actualAudioSampleRate, OUTPUT_SAMPLERATE, 1, 0);
            FloatArbResampler audioResamplerB = new FloatArbResampler(actualAudioSampleRate, OUTPUT_SAMPLERATE, 1, 0);

            //Prepare output audio
            BufferedWaveProvider provider = new BufferedWaveProvider(WaveFormat.CreateIeeeFloatWaveFormat(OUTPUT_SAMPLERATE, 2));
            WaveOut audioOutput = new WaveOut();
            audioOutput.Init(provider);
            audioOutput.Play();

            //Create a managed buffer for use
            byte[] audioOutBuffer = new byte[buffers.bufferSize * 2 * sizeof(float)];

            //Prepare throttle
            SampleThrottle throttle = new SampleThrottle(sampleRate);

            //Work
            state = RadioState.RUNNING;
            while (state == RadioState.RUNNING)
            {
                //Read
                int read = source.Read(buffers.iqBufferPtr, buffers.bufferSize);

                //Add to FFT
                mainFft.AddSamples(buffers.iqBufferPtr, read);

                //Filter, decimate, and demodulate IQ
                int decimatedRead = decimator.Process(buffers.iqBufferPtr, read, buffers.iqBufferPtr, buffers.bufferSize);
                int demodulatedRead = demod.DemodulateStereo(buffers.iqBufferPtr, buffers.audioBBufferPtr, buffers.audioCBufferPtr, decimatedRead);

                //Resample left channel B -> A
                int audioRead = audioResamplerA.Process(buffers.audioBBufferPtr, demodulatedRead, buffers.audioABufferPtr, buffers.bufferSize, false);

                //Resample right channel C -> B
                audioResamplerB.Process(buffers.audioCBufferPtr, demodulatedRead, buffers.audioBBufferPtr, buffers.bufferSize, false);

                //Zipper the two channels together
                fixed(byte* audioOutBufferPtr = audioOutBuffer)
                {
                    float* audioOutBufferPtrFloat = (float*)audioOutBufferPtr;
                    for(int i = 0; i<audioRead; i++)
                    {
                        audioOutBufferPtrFloat[(i * 2) + 0] = buffers.audioABufferPtr[i] * MainLabelingForm.volume;
                        audioOutBufferPtrFloat[(i * 2) + 1] = buffers.audioBBufferPtr[i] * MainLabelingForm.volume;
                    }
                }

                //Write to output
                provider.AddSamples(audioOutBuffer, 0, audioRead * 2 * sizeof(float));

                //Update state
                fftReady = true;

                //Throttle
                throttle.Process(read);
            }

            //Dispose
            fftReady = false;
            audioOutput.Stop();
            audioOutput.Dispose();
            //filter.Dispose();
            demod.Dispose();
            source.Dispose();
            fs.Dispose();

            //Update state
            state = RadioState.STOPPED;
        }

        enum RadioState
        {
            STOPPED,
            RUNNING,
            STARTING,
            STOPPING
        }
    }
}
