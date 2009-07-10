using System;
using System.ComponentModel;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats
{
    [TypeConverter(typeof(VectorTypeConverter))]
    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2()
        {
        }

        public Vector2(Stream input)
        {
            this.Deserialize(input);
        }

        public void Serialize(Stream output)
        {
            output.WriteValueF32(this.X);
            output.WriteValueF32(this.Y);
        }

        public void Deserialize(Stream input)
        {
            this.X = input.ReadValueF32();
            this.Y = input.ReadValueF32();
        }
    }
}
