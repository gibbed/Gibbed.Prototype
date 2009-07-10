using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00010001)]
    public class PolySkin : BaseNode
    {
        public string Name { get; set; }
        public UInt32 Unknown1 { get; set; }
        public string SkeletonName { get; set; }
        public UInt32 Unknown3 { get; set; }

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
            output.WriteStringBASCII(this.Name);
            output.WriteValueU32(this.Unknown1);
            output.WriteStringBASCII(this.SkeletonName);
            output.WriteValueU32(this.Unknown3);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Unknown1 = input.ReadValueU32();
            this.SkeletonName = input.ReadStringBASCII();
            this.Unknown3 = input.ReadValueU32();
        }
    }
}
