using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x07F00000)]
    public class U07F00000 : Node
    {
        public string Unknown1 { get; set; }
        public string Unknown2 { get; set; }
        public string Unknown3 { get; set; }
        public UInt16 Unknown4 { get; set; }
        public UInt16 Unknown5 { get; set; }
        public UInt32 Unknown6 { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteBASCII(this.Unknown1);
            output.WriteBASCII(this.Unknown2);
            output.WriteBASCII(this.Unknown3);
            output.WriteU16(this.Unknown4);
            output.WriteU16(this.Unknown5);
            output.WriteU32(this.Unknown6);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadBASCII();
            this.Unknown2 = input.ReadBASCII();
            this.Unknown3 = input.ReadBASCII();
            this.Unknown4 = input.ReadU16();
            this.Unknown5 = input.ReadU16();
            this.Unknown6 = input.ReadU32();
        }
    }
}
