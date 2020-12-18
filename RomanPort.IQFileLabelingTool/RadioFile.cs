using RomanPort.IQFileLabelingTool.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.IQFileLabelingTool
{
    public class RadioFile
    {
        public RadioFile(string path)
        {
            this.path = path;
            this.metaPath = path + ".meta";

            //Get info
            info = new FileInfo(path);

            //Load metadata
            metadataGenerated = File.Exists(metaPath);
            if (metadataGenerated)
                metadata = File.ReadAllBytes(metaPath);
            else
                metadata = new byte[META_LEN];
        }

        public readonly string path;
        public readonly string metaPath;

        public FileInfo info;

        public bool metadataGenerated;

        private byte[] metadata;
        private const int META_OFF_HEAD = 0;
        private const int META_LEN_HEAD = 8;
        private const int META_OFF_HASH = META_LEN_HEAD;
        private const int META_LEN_HASH = 32;
        private const int META_OFF_RDS = META_OFF_HASH + META_LEN_HASH;
        private const int META_LEN_RDS = 1 + 2 + 64;
        private const int META_OFF_WAVEFORM = META_OFF_RDS + META_LEN_RDS;
        private const int META_LEN_WAVEFORM = FileWaveformGenerator.RESOLUTION * FileWaveformGenerator.BYTES_PER_FRAME;
        private const int META_LEN = META_OFF_WAVEFORM + META_LEN_WAVEFORM;

        public byte[] GetMetaHash()
        {
            //Validate
            if (!metadataGenerated)
                throw new Exception("Metadata is not yet generated.");

            //Read
            byte[] hash = new byte[META_LEN_HASH];
            Array.Copy(metadata, META_OFF_HASH, hash, 0, META_LEN_HASH);
            return hash;
        }

        public byte[] GetMetaWaveform()
        {
            //Validate
            if (!metadataGenerated)
                throw new Exception("Metadata is not yet generated.");

            //Read
            byte[] hash = new byte[META_LEN_WAVEFORM];
            Array.Copy(metadata, META_OFF_WAVEFORM, hash, 0, META_LEN_WAVEFORM);
            return hash;
        }

        public ushort GetMetaRdsPiCode()
        {
            //Validate
            if (!metadataGenerated)
                throw new Exception("Metadata is not yet generated.");

            //Load the UShort for it
            return BitConverter.ToUInt16(metadata, META_OFF_RDS + 1);
        }

        public string GetMetaRdsRadioText()
        {
            //Validate
            if (!metadataGenerated)
                throw new Exception("Metadata is not yet generated.");

            //Read
            return Encoding.ASCII.GetString(metadata, META_OFF_RDS + 3, 64);
        }

        public int GetMetaSampleRate()
        {
            return BitConverter.ToInt32(metadata, 4);
        }

        public bool TryGetMetaTrackInfo(out string title, out string artist, out string stationName)
        {
            //Get the RT
            string rt = GetMetaRdsRadioText();

            //Parse
            return LibSDR.Extras.RDS.RDSClient.TryGetTrackInfo(rt, out title, out artist, out stationName);
        }

        public bool TryGetMetaCallsign(out string callsign)
        {
            //Get PI code
            ushort pi = GetMetaRdsPiCode();

            //If this is zero, don't even attempt
            callsign = null;
            if (pi == 0)
                return false;

            //Try to load
            return LibSDR.Extras.RDS.RDSClient.TryGetCallsign(pi, out callsign);
        }

        public void GenerateMetadata(RadioBuffers buffers, int taskSwap = 0)
        {
            //Task swap is used to switch the CPU intensive waveform generation and the disk intensive hashing to be backward on every other worker thread
            //This should (in theory) speed up the process a lot
            float sampleRate = -1;
            for(int i = 0; i<3; i++)
            {
                int task = (i + taskSwap) % 3;
                switch(task)
                {
                    case 0:
                        //Generate hash
                        byte[] hash = FileHashGenerator.HashFile(path);
                        Array.Copy(hash, 0, metadata, META_OFF_HASH, META_LEN_HASH);
                        break;
                    case 1:
                        //Generate waveform
                        FileWaveformGenerator.GenerateWaveformData(path, metadata, META_OFF_WAVEFORM, META_LEN_WAVEFORM, buffers, out sampleRate);
                        break;
                    case 2:
                        //Do RDS cache
                        metadata[META_OFF_RDS] = (byte)(FileRdsPreloaderGenerator.PreloadRDS(path, metadata, META_OFF_RDS + 3, 30, buffers, out ushort pi) ? 1 : 0);
                        BitConverter.GetBytes(pi).CopyTo(metadata, META_OFF_RDS + 1);
                        break;
                    default:
                        throw new Exception("Unknown task!");
                }
            }

            //Validate
            if (sampleRate == -1)
                throw new Exception("Something went wrong while processing tasks.");

            //Set metadata header
            metadata[0] = (byte)'S';
            metadata[1] = (byte)'M';
            metadata[2] = 0x01;
            metadata[3] = 0x00;
            BitConverter.GetBytes((int)sampleRate).CopyTo(metadata, 4);

            //Save
            File.WriteAllBytes(metaPath, metadata);
            metadataGenerated = true;
        }
    }
}
