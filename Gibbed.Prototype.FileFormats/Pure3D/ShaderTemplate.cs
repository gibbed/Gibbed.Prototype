using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00011009)]
    public class ShaderTemplate : BaseNode
    {
        public string Name { get; set; }
        public UInt32 Unknown02 { get; set; }
        public UInt32 NumPasses { get; set; }
        public UInt32 Unknown04 { get; set; }
        public UInt32 Unknown05 { get; set; }
        public UInt32 NumFloat2 { get; set; }
        public UInt32 Unknown07 { get; set; }
        public UInt32 Unknown08 { get; set; }
        public UInt32 Unknown09 { get; set; }
        public UInt32 NumFloat3 { get; set; }
        public UInt32 Unknown11 { get; set; }
        public UInt32 Unknown12 { get; set; }
        public UInt32 NumFloat4 { get; set; }
        public UInt32 NumMatrices { get; set; }
        public UInt32 NumBools { get; set; }
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
            output.WriteStringBASCII(this.Name);
            output.WriteValueU32(this.Unknown02);
            output.WriteValueU32(this.NumPasses);
            output.WriteValueU32(this.Unknown04);
            output.WriteValueU32(this.Unknown05);
            output.WriteValueU32(this.NumFloat2);
            output.WriteValueU32(this.Unknown07);
            output.WriteValueU32(this.Unknown08);
            output.WriteValueU32(this.Unknown09);
            output.WriteValueU32(this.NumFloat3);
            output.WriteValueU32(this.Unknown11);
            output.WriteValueU32(this.Unknown12);
            output.WriteValueU32(this.NumFloat4);
            output.WriteValueU32(this.NumMatrices);
            output.WriteValueU32(this.NumBools);
            output.WriteValueU32(this.Unknown16);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Unknown02 = input.ReadValueU32();
            this.NumPasses = input.ReadValueU32();
            this.Unknown04 = input.ReadValueU32();
            this.Unknown05 = input.ReadValueU32();
            this.NumFloat2 = input.ReadValueU32();
            this.Unknown07 = input.ReadValueU32();
            this.Unknown08 = input.ReadValueU32();
            this.Unknown09 = input.ReadValueU32();
            this.NumFloat3 = input.ReadValueU32();
            this.Unknown11 = input.ReadValueU32();
            this.Unknown12 = input.ReadValueU32();
            this.NumFloat4 = input.ReadValueU32();
            this.NumMatrices = input.ReadValueU32();
            this.NumBools = input.ReadValueU32();
            this.Unknown16 = input.ReadValueU32();
        }
    }
}
