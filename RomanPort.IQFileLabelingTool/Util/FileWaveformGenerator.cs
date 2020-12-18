using RomanPort.LibSDR.Demodulators;
using RomanPort.LibSDR.Framework.Resamplers.Decimators;
using RomanPort.LibSDR.Framework.Util;
using RomanPort.LibSDR.Sources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.IQFileLabelingTool.Util
{
    public static class FileWaveformGenerator
    {
        public const float DEMOD_BW = 250000;
        public const int BYTES_PER_FRAME = 4;
        public const int RESOLUTION = 1024;
        
        public unsafe static void GenerateWaveformData(string path, byte[] output, int outputIndex, int outputLength, RadioBuffers buffers, out float sampleRate)
        {
            //Open
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using(WavStreamSource source = new WavStreamSource(fs))
            {
                //Open source
                sampleRate = source.Open(buffers.bufferSize);

                //Open decimator
                int decimationRate = SdrFloatDecimator.CalculateDecimationRate(sampleRate, DEMOD_BW, out float actualSampleRate);
                SdrComplexDecimator decimator = new SdrComplexDecimator(decimationRate);

                //Open audio filter
                float[] coefficients = FilterBuilder.MakeLowPassKernel(actualSampleRate, 10, 19000, WindowType.BlackmanHarris4);
                FirFilter audioFilter = new FirFilter(coefficients);

                //Open demodulator
                FmDemodulator demod = new FmDemodulator();
                demod.OnAttached(buffers.bufferSize);
                demod.OnInputSampleRateChanged(actualSampleRate);

                //Calculate
                long skipSamples = source.SampleLength / ((outputLength / BYTES_PER_FRAME) + 1);

                //Process
                for(int i = 0; i<outputLength / BYTES_PER_FRAME; i++)
                {
                    //Seek
                    source.SamplePosition = skipSamples * i;
                    
                    //Read, decimate, and demodulate, and filter
                    int read = source.Read(buffers.iqBufferPtr, Math.Min(buffers.bufferSize, 8192));
                    int decimatedRead = decimator.Process(buffers.iqBufferPtr, read, buffers.iqBufferPtr, buffers.bufferSize);
                    int demodulatedRead = demod.DemodulateMono(buffers.iqBufferPtr, buffers.audioABufferPtr, decimatedRead);
                    audioFilter.Process(buffers.audioABufferPtr, demodulatedRead);

                    //Find the range of samples and write
                    FindMinMax(buffers.audioABufferPtr, demodulatedRead, out float max, out float min);
                    output[outputIndex + (i * BYTES_PER_FRAME) + 0] = (byte)(Math.Min(max, 1) * byte.MaxValue);
                    output[outputIndex + (i * BYTES_PER_FRAME) + 1] = (byte)(Math.Min(-min, 1) * byte.MaxValue);

                    //Do the same thing but remove 8% of outliers
                    int outliersToRemove = (int)(demodulatedRead * 0.08f);
                    for (int r = 0; r < outliersToRemove; r++)
                    {
                        //Find min/max value
                        float localMax = 0;
                        int localMaxIndex = 0;
                        //float localMin = 0;
                        //int localMinIndex = 0;
                        for (int j = 0; j<demodulatedRead; j++)
                        {
                            if(buffers.audioABufferPtr[j] > localMax)
                            {
                                localMax = buffers.audioABufferPtr[j];
                                localMaxIndex = j;
                            }
                            /*if (buffers.audioABufferPtr[j] < localMin)
                            {
                                localMin = buffers.audioABufferPtr[j];
                                localMinIndex = j;
                            }*/
                        }

                        //Remove local min/max
                        buffers.audioABufferPtr[localMaxIndex] = 0;
                        //buffers.audioABufferPtr[localMinIndex] = 0;
                    }

                    //Find the range of samples after outliers were removed and write
                    FindMinMax(buffers.audioABufferPtr, demodulatedRead, out max, out min);
                    output[outputIndex + (i * BYTES_PER_FRAME) + 2] = (byte)(Math.Min(max, 1) * byte.MaxValue);
                    output[outputIndex + (i * BYTES_PER_FRAME) + 3] = (byte)(Math.Min(max, 1) * byte.MaxValue);
                }

                //Dispose
                demod.Dispose();
            }
        }

        private unsafe static void FindMinMax(float* data, int count, out float max, out float min)
        {
            max = 0;
            min = 0;
            for (int j = 0; j < count; j++)
            {
                max = Math.Max(max, data[j]);
                min = Math.Min(min, data[j]);
            }
        }

        public static Bitmap GenerateWaveformImage(byte[] waveform, int width, int height)
        {
            //Create image
            Bitmap img = new Bitmap(width, height);
            int halfImageHeight = img.Height / 2;

            //Decimate to pixel space
            float decimationFactor = (waveform.Length / BYTES_PER_FRAME) / (float)img.Width;
            int[] pixels = new int[img.Width * BYTES_PER_FRAME];
            for(int i = 0; i<RESOLUTION; i++)
            {
                int pixelIndex = (int)(i / decimationFactor) * BYTES_PER_FRAME;
                int waveformIndex = i * BYTES_PER_FRAME;
                for (int c = 0; c < BYTES_PER_FRAME; c++)
                    pixels[pixelIndex + c] = Math.Max(pixels[pixelIndex + c], (int)((waveform[waveformIndex + c] / (float)byte.MaxValue) * halfImageHeight));
            }

            //Generate
            for (int i = 0; i < img.Width; i++)
            {
                //Get point and convert into pixel space
                int index = i * BYTES_PER_FRAME;
                int pointMax = pixels[index + 0];
                int pointMin = pixels[index + 1];
                int pointAvgTop = pixels[index + 2];
                int pointAvgBottom = pixels[index + 3];

                //Draw
                for (int j = 0; j < pointMax; j++)
                    img.SetPixel(i, halfImageHeight - j, Color.Red);
                for (int j = 0; j < pointMin; j++)
                    img.SetPixel(i, halfImageHeight + j, Color.Red);
                for (int j = 0; j < pointAvgTop; j++)
                    img.SetPixel(i, halfImageHeight - j, Color.DarkRed);
                for (int j = 0; j < pointAvgBottom; j++)
                    img.SetPixel(i, halfImageHeight + j, Color.DarkRed);
            }

            return img;
        }
    }
}
