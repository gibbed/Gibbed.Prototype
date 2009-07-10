using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00022000)]
    public class TextureFont : BaseNode
    {
        public UInt32 Version { get; set; }
        public string Name { get; set; }
        public string ShaderName { get; set; }
        public float FontSize { get; set; }
        public float FontWidth { get; set; }
        public float FontHeight { get; set; }
        public float FontBaseLine { get; set; }
        public UInt32 NumTextures { get; set; }

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
            output.WriteStringBASCII(this.ShaderName);
            output.WriteValueF32(this.FontSize);
            output.WriteValueF32(this.FontWidth);
            output.WriteValueF32(this.FontHeight);
            output.WriteValueF32(this.FontBaseLine);
            output.WriteValueU32(this.NumTextures);
        }

        public override void Deserialize(Stream input)
        {
            this.Version = input.ReadValueU32();
            this.Name = input.ReadStringBASCII();
            this.ShaderName = input.ReadStringBASCII();
            this.FontSize = input.ReadValueF32();
            this.FontWidth = input.ReadValueF32();
            this.FontHeight = input.ReadValueF32();
            this.FontBaseLine = input.ReadValueF32();
            this.NumTextures = input.ReadValueU32();
        }
    }
}
