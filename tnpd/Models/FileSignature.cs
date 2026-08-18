using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tnpd.Models
{
    class FileSignature
    {
        public byte[] Signature { get; set; }
        public int Offset { get; set; }

        public FileSignature(byte[] signature, int offset)
        {
            Signature = signature;
            Offset = offset;
        }
    }
}