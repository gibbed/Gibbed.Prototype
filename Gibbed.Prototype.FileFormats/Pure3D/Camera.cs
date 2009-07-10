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
        public UInt32 Version { get; set; }
        public float FOV { get; set; }
        public float AspectRatio { get; set; }
        public float NearClip { get; set; }
        public float FarClip { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Look { get; set; }
        public Vector3 Up { get; set; }

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
            output.WriteValueF32(this.FOV);
            output.WriteValueF32(this.AspectRatio);
            output.WriteValueF32(this.NearClip);
            output.WriteValueF32(this.FarClip);
            this.Position.Serialize(output);
            this.Look.Serialize(output);
            this.Up.Serialize(output);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Version = input.ReadValueU32();
            this.FOV = input.ReadValueF32();
            this.AspectRatio = input.ReadValueF32();
            this.NearClip = input.ReadValueF32();
            this.FarClip = input.ReadValueF32();
            this.Position = new Vector3(input);
            this.Look = new Vector3(input);
            this.Up = new Vector3(input);
        }
    }
}
