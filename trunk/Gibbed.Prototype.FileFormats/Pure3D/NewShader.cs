using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00011015)]
    public class NewShader : BaseNode
    {
        public string Name { get; set; }
        public UInt32 Unknown2 { get; set; }
        public string ShaderTemplateName { get; set; }
        public UInt32 Unknown4 { get; set; }

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
            output.WriteValueU32(this.Unknown2);
            output.WriteStringBASCII(this.ShaderTemplateName);
            output.WriteValueU32(this.Unknown4);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Unknown2 = input.ReadValueU32();
            this.ShaderTemplateName = input.ReadStringBASCII();
            this.Unknown4 = input.ReadValueU32();
        }
    }
}
