using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x0001100B)]
    public class ShaderProgram : BaseNode
    {
        public enum ShaderProgramType : uint
        {
            VertexShader = 0,
            PixelShader = 1
        }

        public string Name { get; set; }
        public UInt32 Unknown02 { get; set; }
        public ShaderProgramType ShaderType { get; set; }
        public UInt32 ChildCount2 { get; set; }

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
            output.WriteValueU32((UInt32)this.ShaderType);
            output.WriteValueU32(this.ChildCount2);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Unknown02 = input.ReadValueU32();
            this.ShaderType = (ShaderProgramType)input.ReadValueU32();
            this.ChildCount2 = input.ReadValueU32();
        }
    }
}
