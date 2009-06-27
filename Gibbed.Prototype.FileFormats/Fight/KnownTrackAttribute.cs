using System;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class KnownTrackAttribute : KnownHashForContextAttribute
    {
        public KnownTrackAttribute(Type type, UInt64 hash)
            : base(type, hash)
        {
        }

        public KnownTrackAttribute(Type type, string name)
            : base(type, name)
        {
        }
    }
}
