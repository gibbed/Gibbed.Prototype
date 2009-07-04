using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00015880)]
    public class ParticleSystem : BaseNode
    {
        public UInt32 Unknown1 { get; set; }
        public string Name { get; set; }
        public UInt32 Unknown3 { get; set; }
        public UInt32 Unknown4 { get; set; }
        public UInt32 Unknown5 { get; set; }
        public Vector4 Unknown6 { get; set; }
        public Vector3 Unknown7 { get; set; }
        public UInt32 Unknown8 { get; set; }

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
            output.WriteU32(this.Unknown3);
            output.WriteU32(this.Unknown4);
            output.WriteU32(this.Unknown5);
            this.Unknown6.Serialize(output);
            this.Unknown7.Serialize(output);
            output.WriteU32(this.Unknown8);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadU32();
            this.Name = input.ReadBASCII();
            this.Unknown3 = input.ReadU32();
            this.Unknown4 = input.ReadU32();
            this.Unknown5 = input.ReadU32();
            this.Unknown6 = new Vector4();
            this.Unknown6.Deserialize(input);
            this.Unknown7 = new Vector3();
            this.Unknown7.Deserialize(input);
            this.Unknown8 = input.ReadU32();
        }
    }
}
