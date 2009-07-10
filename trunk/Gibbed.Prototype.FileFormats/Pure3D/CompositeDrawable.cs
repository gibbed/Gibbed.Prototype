using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00123000)]
    public class CompositeDrawable : BaseNode
    {
        public UInt32 Version { get; set; }
        public string Name { get; set; }
        public string SkeletonName { get; set; }
        public UInt32 NumPrimitives { get; set; }

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
            output.WriteStringBASCII(this.SkeletonName);
            output.WriteValueU32(this.NumPrimitives);
        }

        public override void Deserialize(Stream input)
        {
            this.Version = input.ReadValueU32();
            this.Name = input.ReadStringBASCII();
            this.SkeletonName = input.ReadStringBASCII();
            this.NumPrimitives = input.ReadValueU32();
        }
    }
}
