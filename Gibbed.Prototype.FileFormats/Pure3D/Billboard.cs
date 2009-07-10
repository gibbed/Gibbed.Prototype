using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00017006)]
    public class Billboard : BaseNode
    {
        public UInt32 Version { get; set; }
        public string Name { get; set; }
        public string NewShaderName { get; set; }
        public UInt32 CutOffEnabled { get; set; }
        public UInt32 ZTest { get; set; }
        public UInt32 ZWrite { get; set; }
        public UInt32 OcclusionCulling { get; set; }
        public UInt32 NumQuads { get; set; }

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
            output.WriteValueU32(this.Version);
            output.WriteStringBASCII(this.Name);
            output.WriteStringBASCII(this.NewShaderName);
            output.WriteValueU32(this.CutOffEnabled);
            output.WriteValueU32(this.ZTest);
            output.WriteValueU32(this.ZWrite);
            output.WriteValueU32(this.OcclusionCulling);
            output.WriteValueU32(this.NumQuads);
        }

        public override void Deserialize(Stream input)
        {
            this.Version = input.ReadValueU32();
            this.Name = input.ReadStringBASCII();
            this.NewShaderName = input.ReadStringBASCII();
            this.CutOffEnabled = input.ReadValueU32();
            this.ZTest = input.ReadValueU32();
            this.ZWrite = input.ReadValueU32();
            this.OcclusionCulling = input.ReadValueU32();
            this.NumQuads = input.ReadValueU32();
        }
    }
}
