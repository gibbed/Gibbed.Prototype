using System;
using System.ComponentModel;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats
{
    [TypeConverter(typeof(VectorTypeConverter))]
    public class Vector4
    {
        public float W { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector4()
        {
        }

        public Vector4(Stream input)
        {
            this.Deserialize(input);
        }

        public void Serialize(Stream output)
        {
            output.WriteValueF32(this.W);
            output.WriteValueF32(this.X);
            output.WriteValueF32(this.Y);
            output.WriteValueF32(this.Z);
        }

        public void Deserialize(Stream input)
        {
            this.W = input.ReadValueF32();
            this.X = input.ReadValueF32();
            this.Y = input.ReadValueF32();
            this.Z = input.ReadValueF32();
        }
    }
}
