using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00022000)]
    public class TextureFont : BaseNode
    {
        public UInt32 Unknown1 { get; set; }
        public string Name { get; set; }
        public string Unknown2 { get; set; }
        public float Unknown3 { get; set; }
        public float Unknown4 { get; set; }
        public float Unknown5 { get; set; }
        public float Unknown6 { get; set; }
        public UInt32 Unknown7 { get; set; }

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
            output.WriteValueU32(this.Unknown1);
            output.WriteBASCII(this.Name);
            output.WriteBASCII(this.Unknown2);
            output.WriteValueF32(this.Unknown3);
            output.WriteValueF32(this.Unknown4);
            output.WriteValueF32(this.Unknown5);
            output.WriteValueF32(this.Unknown6);
            output.WriteValueU32(this.Unknown7);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadValueU32();
            this.Name = input.ReadBASCII();
            this.Unknown2 = input.ReadBASCII();
            this.Unknown3 = input.ReadValueF32();
            this.Unknown4 = input.ReadValueF32();
            this.Unknown5 = input.ReadValueF32();
            this.Unknown6 = input.ReadValueF32();
            this.Unknown7 = input.ReadValueU32();
        }
    }
}
