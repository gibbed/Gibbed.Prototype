using System;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight
{
    public class KnownConditionAttribute : Attribute
    {
        public Type BranchType;
        public UInt64 Hash;

        private KnownConditionAttribute(Type branchType)
        {
            if (branchType.IsSubclassOf(typeof(BranchBase)) == false)
            {
                throw new InvalidOperationException("invalid condition association");
            }

            this.BranchType = branchType;
        }

        public KnownConditionAttribute(Type branchType, UInt64 hash)
            : this(branchType)
        {
            this.Hash = hash;
        }

        public KnownConditionAttribute(Type branchType, string name)
            : this(branchType)
        {
            this.Hash = name.Hash1003F();
        }
    }
}
