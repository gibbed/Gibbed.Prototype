using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00021001)]
    public class ExpressionGroup : Node
    {
        public UInt32 Unknown1 { get; set; }
        public string Unknown2 { get; set; }
        public string Unknown3 { get; set; }
        public UInt32[] Unknown4 { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteU32(this.Unknown1);
            output.WriteBASCII(this.Unknown2);
            output.WriteBASCII(this.Unknown3);
            output.WriteS32(this.Unknown4.Length);
            for (int i = 0; i < this.Unknown4.Length; i++)
            {
                output.WriteU32(this.Unknown4[i]);
            }
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadU32();
            this.Unknown2 = input.ReadBASCII();
            this.Unknown3 = input.ReadBASCII();

            int count = input.ReadS32();
            this.Unknown4 = new UInt32[count];
            for (int i = 0; i < count; i++)
            {
                this.Unknown4[i] = input.ReadU32();
            }
        }
    }
}
