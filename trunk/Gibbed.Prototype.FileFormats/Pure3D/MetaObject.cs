using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x07F00000)]
    public class MetaObject : Node
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string TypeName { get; set; }
        public UInt16 Unknown4 { get; set; }
        public UInt16 Unknown5 { get; set; }
        public UInt32 Unknown6 { get; set; }

        public override string ToString()
        {
            if (this.LongName == null || this.LongName.Length == 0)
            {
                return base.ToString();
            }

            return base.ToString() + " (" + this.LongName + ")";
        }

        public override void Serialize(Stream output)
        {
            output.WriteBASCII(this.LongName);
            output.WriteBASCII(this.ShortName);
            output.WriteBASCII(this.TypeName);
            output.WriteU16(this.Unknown4);
            output.WriteU16(this.Unknown5);
            output.WriteU32(this.Unknown6);
        }

        public override void Deserialize(Stream input)
        {
            this.LongName = input.ReadBASCII();
            this.ShortName = input.ReadBASCII();
            this.TypeName = input.ReadBASCII();
            this.Unknown4 = input.ReadU16();
            this.Unknown5 = input.ReadU16();
            this.Unknown6 = input.ReadU32();
        }
    }
}
