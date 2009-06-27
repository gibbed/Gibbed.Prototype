using System;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight
{
    public class KnownBranchAttribute : KnownHashAttribute
    {
        public KnownBranchAttribute(UInt64 hash)
            : base(hash)
        {
        }

        public KnownBranchAttribute(string name)
            : base(name)
        {
        }
    }
}
