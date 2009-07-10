using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00121000)]
    public class Animation : BaseNode
    {
        public UInt32 Version { get; set; }
        public string Name { get; set; }
        public FourCC AnimationType { get; set; }
        public float NumFrames { get; set; }
        public float FrameRate { get; set; }
        public UInt32 Cyclic { get; set; }

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
            this.AnimationType.Serialize(output);
            output.WriteValueF32(this.NumFrames);
            output.WriteValueF32(this.FrameRate);
            output.WriteValueU32(this.Cyclic);
        }

        public override void Deserialize(Stream input)
        {
            this.Version = input.ReadValueU32();
            this.Name = input.ReadStringBASCII();
            this.AnimationType = new FourCC(input);
            this.NumFrames = input.ReadValueF32();
            this.FrameRate = input.ReadValueF32();
            this.Cyclic = input.ReadValueU32();
        }
    }
}
