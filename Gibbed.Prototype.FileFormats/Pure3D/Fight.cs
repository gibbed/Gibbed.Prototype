using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x20000701)]
    public class Fight : Node
    {
        public string Name { get; set; }
        public UInt16 Unknown2 { get; set; }
        public string DataType { get; set; }
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
            output.WriteBASCII(this.Name);
            output.WriteU16(this.Unknown2);
            output.WriteBASCII(this.DataType);
            output.WriteU32(this.Unknown4);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadBASCII();
            this.Unknown2 = input.ReadU16();
            this.DataType = input.ReadBASCII();
            this.Unknown4 = input.ReadU32();
        }
    }
}
