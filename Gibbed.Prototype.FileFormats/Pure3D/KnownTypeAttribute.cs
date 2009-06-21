using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
