using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00021002)]
    public class ExpressionMixer : BaseNode
    {
        public UInt32 Unknown1 { get; set; }
        public string Name { get; set; }
        public UInt32 Unknown3 { get; set; }
        public string CompositeDrawableName { get; set; }
        public string ExpressionGroupName { get; set; }

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
            output.WriteStringBASCII(this.Name);
            output.WriteValueU32(this.Unknown3);
            output.WriteStringBASCII(this.CompositeDrawableName);
            output.WriteStringBASCII(this.ExpressionGroupName);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadValueU32();
            this.Name = input.ReadStringBASCII();
            this.Unknown3 = input.ReadValueU32();
            this.CompositeDrawableName = input.ReadStringBASCII();
            this.ExpressionGroupName = input.ReadStringBASCII();
        }
    }
}
