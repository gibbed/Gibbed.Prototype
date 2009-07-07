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
            output.WriteValueU32(this.Unknown1);
            output.WriteValueU32(this.Unknown2);
            output.WriteBASCII(this.PolySkinName);
            output.WriteValueU32(this.Unknown4);
            output.WriteValueU32(this.Unknown5);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadValueU32();
            this.Unknown2 = input.ReadValueU32();
            this.PolySkinName = input.ReadBASCII();
            this.Unknown4 = input.ReadValueU32();
            this.Unknown5 = input.ReadValueU32();
        }
    }
}
