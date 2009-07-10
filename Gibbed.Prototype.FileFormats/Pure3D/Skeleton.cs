using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00023000)]
    public class Skeleton : BaseNode
    {
        public string Name { get; set; }
        public UInt32 Version { get; set; }
        public UInt32 NumJoints { get; set; }
        public UInt32 NumPartitions { get; set; }
        public UInt32 NumLimbs { get; set; }

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
            output.WriteValueU32(this.Version);
            output.WriteValueU32(this.NumJoints);
            output.WriteValueU32(this.NumPartitions);
            output.WriteValueU32(this.NumLimbs);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Version = input.ReadValueU32();
            this.NumJoints = input.ReadValueU32();
            this.NumPartitions = input.ReadValueU32();
            this.NumLimbs = input.ReadValueU32();
        }
    }
}
