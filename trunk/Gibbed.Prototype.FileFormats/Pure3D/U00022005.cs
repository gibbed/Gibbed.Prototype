using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00022005)]
    public class U00022005 : Node
    {
        public UInt32 Unknown { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteU32(this.Unknown);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown = input.ReadU32();
        }
    }
}
