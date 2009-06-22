using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00123000)]
    public class CompositeDrawable : Node
    {
        public UInt32 Unknown1 { get; set; }
        public string Unknown2 { get; set; }
        public string Unknown3 { get; set; }
        public UInt32 Unknown4 { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteU32(this.Unknown1);
            output.WriteBASCII(this.Unknown2);
            output.WriteBASCII(this.Unknown3);
            output.WriteU32(this.Unknown4);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadU32();
            this.Unknown2 = input.ReadBASCII();
            this.Unknown3 = input.ReadBASCII();
            this.Unknown4 = input.ReadU32();
        }
    }
}
