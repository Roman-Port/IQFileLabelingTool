using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RomanPort.IQFileLabelingTool.Util
{
    public static class FileHashGenerator
    {
        public static byte[] HashFile(string path)
        {
            //Oppn file and begin hashing
            byte[] hash;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (SHA256Managed a = (SHA256Managed)SHA256Managed.Create())
            {
                //Similar to ComputeHash in https://referencesource.microsoft.com/#mscorlib/system/security/cryptography/hashalgorithm.cs,e7c6be1ed86f474f
                //We use a (much) larger buffer to help speed things up
                byte[] buffer = new byte[32768];
                int bytesRead;
                do
                {
                    bytesRead = fs.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        a.TransformBlock(buffer, 0, bytesRead, null, 0);
                    }
                } while (bytesRead > 0);
                a.TransformFinalBlock(new byte[0], 0, 0);
                hash = a.Hash;
            }
            return hash;
        }
    }
}
