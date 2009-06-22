using System;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00011020)]
    public class U00011020 : Node
    {
        public UInt16 Unknown1 { get; set; }
        public UInt16 Unknown2 { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteU16(this.Unknown1);
            output.WriteU16(this.Unknown2);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadU16();
            this.Unknown2 = input.ReadU16();
        }
    }
}
