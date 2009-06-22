using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x0001100B)]
    public class ShaderProgram : Node
    {
        public string Name { get; set; }
        public UInt32 Unknown02 { get; set; }
        public UInt32 Unknown03 { get; set; }
        public UInt32 Unknown04 { get; set; }

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
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadBASCII();
            this.Unknown02 = input.ReadU32();
            this.Unknown03 = input.ReadU32();
            this.Unknown04 = input.ReadU32();
        }
    }
}
