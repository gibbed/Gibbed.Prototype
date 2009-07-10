using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00021001)]
    public class ExpressionGroup : BaseNode
    {
        public UInt32 Version { get; set; }
        public string Name { get; set; }
        public string CompositeDrawableName { get; set; }
        public Int32 Count { get; set; }
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
            output.WriteValueU32(this.Version);
            output.WriteStringBASCII(this.Name);
            output.WriteStringBASCII(this.CompositeDrawableName);
            output.WriteValueS32(this.Unknown4.Length);
            for (int i = 0; i < this.Unknown4.Length; i++)
            {
                output.WriteValueU32(this.Unknown4[i]);
            }
        }

        public override void Deserialize(Stream input)
        {
            this.Version = input.ReadValueU32();
            this.Name = input.ReadStringBASCII();
            this.CompositeDrawableName = input.ReadStringBASCII();

            this.Count = input.ReadValueS32();
            this.Unknown4 = new UInt32[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                this.Unknown4[i] = input.ReadValueU32();
            }
        }
    }
}
