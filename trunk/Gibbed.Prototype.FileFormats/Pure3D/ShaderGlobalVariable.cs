using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x0001100D)]
    public class ShaderGlobalVariable : BaseNode
    {
        public string Name { get; set; }
        public ShaderVariableType VariableType { get; set; }
        public UInt32 Register { get; set; }

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
            output.WriteValueU32((UInt32)this.VariableType);
            output.WriteValueU32(this.Register);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.VariableType = (ShaderVariableType)input.ReadValueU32();
            this.Register = input.ReadValueU32();
        }
    }
}
