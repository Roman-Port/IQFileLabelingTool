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
    public static class FileRdsPreloaderGenerator
    {
        public const int DEMOD_BW = 250000;
        
        public unsafe static bool PreloadRDS(string path, byte[] rtBuffer, int rtBufferOffset, int maxRuntime, RadioBuffers buffers, out ushort piCode)
        {
            //Open source
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            WavStreamSource source = new WavStreamSource(fs);
            float sampleRate = source.Open(buffers.bufferSize);

            //Jump to the middle of the file for better RDS text
            source.SamplePosition = source.SampleLength / 2;

            //Open decimator
            int decimationRate = SdrFloatDecimator.CalculateDecimationRate(sampleRate, DEMOD_BW, out float actualSampleRate);
            SdrComplexDecimator decimator = new SdrComplexDecimator(decimationRate);

            //Open demodulator
            WbFmDemodulator demod = new WbFmDemodulator();
            demod.OnAttached(buffers.bufferSize);
            demod.OnInputSampleRateChanged(actualSampleRate);

            //Open RDS
            bool success = false;
            var rds = demod.UseRds();
            rds.OnRtTextUpdated += (LibSDR.Extras.RDS.RDSClient client, string text) =>
            {
                //Copy to RT buffer
                for (int i = 0; i < 64; i++)
                    rtBuffer[rtBufferOffset + i] = (byte)client.rtBuffer[i];

                //Set success flag
                success = true;
            };

            //Process
            double readTarget = maxRuntime * sampleRate;
            long readTotal = 0;
            int read;
            do
            {
                //Read, filter, decimate, and demodulate RDS
                read = source.Read(buffers.iqBufferPtr, buffers.bufferSize);
                int decimatedRead = decimator.Process(buffers.iqBufferPtr, read, buffers.iqBufferPtr, buffers.bufferSize);
                demod.DemodulateRDS(buffers.iqBufferPtr, decimatedRead);
                readTotal += read;
            } while (readTotal < readTarget && !success && read != 0);

            //Dispose
            demod.Dispose();
            source.Dispose();
            fs.Dispose();

            //Set PI code
            piCode = rds.piCode;

            return success;
        }
    }
}
