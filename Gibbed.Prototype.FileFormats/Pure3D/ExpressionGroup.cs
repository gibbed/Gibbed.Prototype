using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00021001)]
    public class ExpressionGroup : BaseNode
    {
        public UInt32 Unknown1 { get; set; }
        public string Name { get; set; }
        public string CompositeDrawableName { get; set; }
        public UInt32[] Unknown4 { get; set; }

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
            output.WriteBASCII(this.CompositeDrawableName);
            output.WriteValueS32(this.Unknown4.Length);
            for (int i = 0; i < this.Unknown4.Length; i++)
            {
                output.WriteValueU32(this.Unknown4[i]);
            }
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadValueU32();
            this.Name = input.ReadBASCII();
            this.CompositeDrawableName = input.ReadBASCII();

            int count = input.ReadValueS32();
            this.Unknown4 = new UInt32[count];
            for (int i = 0; i < count; i++)
            {
                this.Unknown4[i] = input.ReadValueU32();
            }
        }
    }
}
