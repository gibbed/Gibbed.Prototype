using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00011009)]
    public class ShaderTemplate : Node
    {
        public string Name { get; set; }
        public UInt32 Unknown02 { get; set; }
        public UInt32 Unknown03 { get; set; }
        public UInt32 Unknown04 { get; set; }
        public UInt32 Unknown05 { get; set; }
        public UInt32 Unknown06 { get; set; }
        public UInt32 Unknown07 { get; set; }
        public UInt32 Unknown08 { get; set; }
        public UInt32 Unknown09 { get; set; }
        public UInt32 Unknown10 { get; set; }
        public UInt32 Unknown11 { get; set; }
        public UInt32 Unknown12 { get; set; }
        public UInt32 Unknown13 { get; set; }
        public UInt32 Unknown14 { get; set; }
        public UInt32 Unknown15 { get; set; }
        public UInt32 Unknown16 { get; set; }

        public override string ToString()
        {
            if (this.Name == null || this.Name.Length == 0)
            {
                return base.ToString();
            }

            return base.ToString() + " (" + this.Name + ")";
        }

        public override void Serialize(Stream output)
        {
            output.WriteBASCII(this.Name);
            output.WriteU32(this.Unknown02);
            output.WriteU32(this.Unknown03);
            output.WriteU32(this.Unknown04);
            output.WriteU32(this.Unknown05);
            output.WriteU32(this.Unknown06);
            output.WriteU32(this.Unknown07);
            output.WriteU32(this.Unknown08);
            output.WriteU32(this.Unknown09);
            output.WriteU32(this.Unknown10);
            output.WriteU32(this.Unknown11);
            output.WriteU32(this.Unknown12);
            output.WriteU32(this.Unknown13);
            output.WriteU32(this.Unknown14);
            output.WriteU32(this.Unknown15);
            output.WriteU32(this.Unknown16);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadBASCII();
            this.Unknown02 = input.ReadU32();
            this.Unknown03 = input.ReadU32();
            this.Unknown04 = input.ReadU32();
            this.Unknown05 = input.ReadU32();
            this.Unknown06 = input.ReadU32();
            this.Unknown07 = input.ReadU32();
            this.Unknown08 = input.ReadU32();
            this.Unknown09 = input.ReadU32();
            this.Unknown10 = input.ReadU32();
            this.Unknown11 = input.ReadU32();
            this.Unknown12 = input.ReadU32();
            this.Unknown13 = input.ReadU32();
            this.Unknown14 = input.ReadU32();
            this.Unknown15 = input.ReadU32();
            this.Unknown16 = input.ReadU32();
        }
    }
}
