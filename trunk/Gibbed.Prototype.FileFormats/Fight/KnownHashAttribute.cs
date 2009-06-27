using System;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight
{
    public abstract class KnownHashAttribute : Attribute
    {
        public UInt64 Hash;

        public KnownHashAttribute(UInt64 hash)
        {
            this.Hash = hash;
        }

        public KnownHashAttribute(string name)
        {
            this.Hash = name.Hash1003F();
            FightHashes.Register(name);
        }
    }
}
