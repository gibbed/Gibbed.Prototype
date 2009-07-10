using System;
using System.ComponentModel;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3()
        {
        }

        public Vector3(Stream input)
        {
            this.Deserialize(input);
        }

        public void Serialize(Stream output)
        {
            output.WriteValueF32(this.X);
            output.WriteValueF32(this.Y);
            output.WriteValueF32(this.Z);
        }

        public void Deserialize(Stream input)
        {
            this.X = input.ReadValueF32();
            this.Y = input.ReadValueF32();
            this.Z = input.ReadValueF32();
        }
    }
}
