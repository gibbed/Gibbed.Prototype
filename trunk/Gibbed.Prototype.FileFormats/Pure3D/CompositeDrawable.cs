using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00123000)]
    public class CompositeDrawable : BaseNode
    {
        public UInt32 Unknown1 { get; set; }
        public string Name { get; set; }
        public string SkeletonName { get; set; }
        public UInt32 PolySkinReferenceCount { get; set; }

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
            output.WriteU32(this.Unknown1);
            output.WriteBASCII(this.Name);
            output.WriteBASCII(this.SkeletonName);
            output.WriteU32(this.PolySkinReferenceCount);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadU32();
            this.Name = input.ReadBASCII();
            this.SkeletonName = input.ReadBASCII();
            this.PolySkinReferenceCount = input.ReadU32();
        }
    }
}
