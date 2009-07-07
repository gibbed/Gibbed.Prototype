using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00002200)]
    public class Camera : BaseNode
    {
        public string Name { get; set; }
        public UInt32 Unknown2 { get; set; }
        public float Unknown3 { get; set; }
        public float Unknown4 { get; set; }
        public float Unknown5 { get; set; }
        public float Unknown6 { get; set; }
        public Vector3 Unknown7 { get; set; }
        public Vector3 Unknown8 { get; set; }
        public Vector3 Unknown9 { get; set; }

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
            output.WriteValueU32(this.Unknown2);
            output.WriteValueF32(this.Unknown3);
            output.WriteValueF32(this.Unknown4);
            output.WriteValueF32(this.Unknown5);
            output.WriteValueF32(this.Unknown6);
            this.Unknown7.Serialize(output);
            this.Unknown8.Serialize(output);
            this.Unknown9.Serialize(output);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Unknown2 = input.ReadValueU32();
            this.Unknown3 = input.ReadValueF32();
            this.Unknown4 = input.ReadValueF32();
            this.Unknown5 = input.ReadValueF32();
            this.Unknown6 = input.ReadValueF32();
            this.Unknown7 = new Vector3();
            this.Unknown7.Deserialize(input);
            this.Unknown8 = new Vector3();
            this.Unknown8.Deserialize(input);
            this.Unknown9 = new Vector3();
            this.Unknown9.Deserialize(input);
        }
    }
}
