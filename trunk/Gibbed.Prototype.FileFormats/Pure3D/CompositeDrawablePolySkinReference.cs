using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00123001)]
    public class CompositeDrawablePolySkinReference : BaseNode
    {
        public UInt32 Unknown1 { get; set; }
        public UInt32 Unknown2 { get; set; }
        public string PolySkinName { get; set; }
        public UInt32 Unknown4 { get; set; }
        public UInt32 Unknown5 { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteU32(this.Unknown1);
            output.WriteU32(this.Unknown2);
            output.WriteBASCII(this.PolySkinName);
            output.WriteU32(this.Unknown4);
            output.WriteU32(this.Unknown5);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadU32();
            this.Unknown2 = input.ReadU32();
            this.PolySkinName = input.ReadBASCII();
            this.Unknown4 = input.ReadU32();
            this.Unknown5 = input.ReadU32();
        }
    }
}
