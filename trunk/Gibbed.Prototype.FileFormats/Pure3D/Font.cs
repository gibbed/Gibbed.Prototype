using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00022000)]
    public class Font : Node
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
            return "Font (" + this.Name.ToString() + ")";
        }

        public override void Serialize(Stream output)
        {
            output.WriteU32(this.Unknown1);
            output.WriteBASCII(this.Name);
            output.WriteBASCII(this.Unknown2);
            output.WriteF32(this.Unknown3);
            output.WriteF32(this.Unknown4);
            output.WriteF32(this.Unknown5);
            output.WriteF32(this.Unknown6);
            output.WriteU32(this.Unknown7);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadU32();
            this.Name = input.ReadBASCII();
            this.Unknown2 = input.ReadBASCII();
            this.Unknown3 = input.ReadF32();
            this.Unknown4 = input.ReadF32();
            this.Unknown5 = input.ReadF32();
            this.Unknown6 = input.ReadF32();
            this.Unknown7 = input.ReadU32();
        }
    }
}
