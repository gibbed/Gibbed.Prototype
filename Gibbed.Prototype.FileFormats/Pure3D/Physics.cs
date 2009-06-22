using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x07020000)]
    public class Physics : Node
    {
        public string Unknown1 { get; set; }
        public UInt32 Unknown2 { get; set; }
        public UInt32 Unknown3 { get; set; }
        public UInt32 Unknown4 { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteBASCII(this.Unknown1);
            output.WriteU32(this.Unknown2);
            output.WriteU32(this.Unknown3);
            output.WriteU32(this.Unknown4);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadBASCII();
            this.Unknown2 = input.ReadU32();
            this.Unknown3 = input.ReadU32();
            this.Unknown4 = input.ReadU32();
        }
    }
}
