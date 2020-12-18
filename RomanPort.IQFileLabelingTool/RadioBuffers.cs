using RomanPort.LibSDR.Framework;
using RomanPort.LibSDR.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.IQFileLabelingTool
{
    public unsafe class RadioBuffers : IDisposable
    {
        public readonly int bufferSize;
        public UnsafeBuffer iqBuffer;
        public Complex* iqBufferPtr;
        public UnsafeBuffer audioABuffer;
        public float* audioABufferPtr;
        public UnsafeBuffer audioBBuffer;
        public float* audioBBufferPtr;
        public UnsafeBuffer audioCBuffer;
        public float* audioCBufferPtr;

        public RadioBuffers(int bufferSize)
        {
            this.bufferSize = bufferSize;
            iqBuffer = UnsafeBuffer.Create(bufferSize, sizeof(Complex));
            audioABuffer = UnsafeBuffer.Create(bufferSize, sizeof(float));
            audioBBuffer = UnsafeBuffer.Create(bufferSize, sizeof(float));
            audioCBuffer = UnsafeBuffer.Create(bufferSize, sizeof(float));
            iqBufferPtr = (Complex*)iqBuffer;
            audioABufferPtr = (float*)audioABuffer;
            audioBBufferPtr = (float*)audioBBuffer;
            audioCBufferPtr = (float*)audioCBuffer;
        }

        public void Dispose()
        {
            iqBuffer.Dispose();
            audioABuffer.Dispose();
            audioBBuffer.Dispose();
            audioCBuffer.Dispose();
        }
    }
}
