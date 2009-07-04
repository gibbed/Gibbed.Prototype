using System.IO;
using Gibbed.Prototype.Helpers;
using Gibbed.Helpers;
using System;
using System.Globalization;
using System.Drawing;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00010020)]
    public class U00010020 : BaseNode
    {
        public UInt32 Unknown01 { get; set; }
        public string ShaderName { get; set; }
        public UInt32 Unknown03 { get; set; }
        public UInt32 Unknown04 { get; set; }
        public UInt32 Unknown05 { get; set; }
        public UInt32 Unknown06 { get; set; }
        public UInt32 Unknown07 { get; set; }
        public UInt32 Unknown08 { get; set; }
        public UInt32 Unknown09 { get; set; }
        public UInt32 Unknown10 { get; set; }
        public UInt32 Unknown11 { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteU32(this.Unknown01);
            output.WriteBASCII(this.ShaderName);
            output.WriteU32(this.Unknown03);
            output.WriteU32(this.Unknown04);
            output.WriteU32(this.Unknown05);
            output.WriteU32(this.Unknown06);
            output.WriteU32(this.Unknown07);
            output.WriteU32(this.Unknown08);
            output.WriteU32(this.Unknown09);
            output.WriteU32(this.Unknown10);
            output.WriteU32(this.Unknown11);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown01 = input.ReadU32();
            this.ShaderName = input.ReadBASCII();
            this.Unknown03 = input.ReadU32();
            this.Unknown04 = input.ReadU32();
            this.Unknown05 = input.ReadU32();
            this.Unknown06 = input.ReadU32();
            this.Unknown07 = input.ReadU32();
            this.Unknown08 = input.ReadU32();
            this.Unknown09 = input.ReadU32();
            this.Unknown10 = input.ReadU32();
            this.Unknown11 = input.ReadU32();
        }
    }
}
