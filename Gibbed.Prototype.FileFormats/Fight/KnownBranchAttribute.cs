using System;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight
{
    public class KnownBranchAttribute : Attribute
    {
        public Type ContextType;
        public UInt64 Hash;

        private KnownBranchAttribute(Type contextType)
        {
            if (contextType.IsSubclassOf(typeof(ContextBase)) == false)
            {
                throw new InvalidOperationException("invalid branch association");
            }

            this.ContextType = contextType;
        }

        public KnownBranchAttribute(Type contextType, UInt64 hash)
            : this(contextType)
        {
            this.Hash = hash;
        }

        public KnownBranchAttribute(Type contextType, string name)
            : this(contextType)
        {
            this.Hash = name.Hash1003F();
        }
    }
}
