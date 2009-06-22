using System;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    public class KnownTypeAttribute : Attribute
    {
		public UInt32 Id;
        public KnownTypeAttribute(UInt32 id)
		{
            this.Id = id;
		}
    }
}
