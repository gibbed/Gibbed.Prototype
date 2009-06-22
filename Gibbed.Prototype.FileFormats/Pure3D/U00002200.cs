using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00002200)]
    public class U00002200 : Node
    {
        public string Unknown1 { get; set; }
        public UInt32 Unknown2 { get; set; }
        public float Unknown3 { get; set; }
        public float Unknown4 { get; set; }
        public float Unknown5 { get; set; }
        public float Unknown6 { get; set; }
        public float[] Unknown7 { get; set; }
        public float[] Unknown8 { get; set; }
        public float[] Unknown9 { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteBASCII(this.Unknown1);
            output.WriteU32(this.Unknown2);
            output.WriteF32(this.Unknown3);
            output.WriteF32(this.Unknown4);
            output.WriteF32(this.Unknown5);
            output.WriteF32(this.Unknown6);
            output.WriteF32(this.Unknown7[0]);
            output.WriteF32(this.Unknown7[1]);
            output.WriteF32(this.Unknown7[2]);
            output.WriteF32(this.Unknown8[0]);
            output.WriteF32(this.Unknown8[1]);
            output.WriteF32(this.Unknown8[2]);
            output.WriteF32(this.Unknown9[0]);
            output.WriteF32(this.Unknown9[1]);
            output.WriteF32(this.Unknown9[2]);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadBASCII();
            this.Unknown2 = input.ReadU32();
            this.Unknown3 = input.ReadF32();
            this.Unknown4 = input.ReadF32();
            this.Unknown5 = input.ReadF32();
            this.Unknown6 = input.ReadF32();
            this.Unknown7 = new float[3];
            this.Unknown7[0] = input.ReadF32();
            this.Unknown7[1] = input.ReadF32();
            this.Unknown7[2] = input.ReadF32();
            this.Unknown8 = new float[3];
            this.Unknown8[0] = input.ReadF32();
            this.Unknown8[1] = input.ReadF32();
            this.Unknown8[2] = input.ReadF32();
            this.Unknown9 = new float[3];
            this.Unknown9[0] = input.ReadF32();
            this.Unknown9[1] = input.ReadF32();
            this.Unknown9[2] = input.ReadF32();
        }
    }
}
