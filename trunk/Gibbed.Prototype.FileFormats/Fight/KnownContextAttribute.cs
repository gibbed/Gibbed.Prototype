using System;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight
{
    public class KnownContextAttribute : KnownHashAttribute
    {
        public KnownContextAttribute(UInt64 hash)
            : base(hash)
        {
        }

        public KnownContextAttribute(string name)
            : base(name)
        {
        }
    }
}
