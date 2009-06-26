using System;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight
{
    public class KnownContextAttribute : Attribute
    {
        public UInt64 Hash;
        
        public KnownContextAttribute(UInt64 hash)
        {
            this.Hash = hash;
        }

        public KnownContextAttribute(string name)
        {
            this.Hash = name.Hash1003F();
        }
    }
}
