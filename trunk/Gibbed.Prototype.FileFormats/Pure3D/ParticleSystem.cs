using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00015880)]
    public class ParticleSystem : BaseNode
    {
        public UInt32 Version { get; set; }
        public string Name { get; set; }
        public UInt32 Unknown3 { get; set; }
        public UInt32 Unknown4 { get; set; }
        public UInt32 Unknown5 { get; set; }
        public Vector4 Rotation { get; set; }
        public Vector3 Translation { get; set; }
        public UInt32 NumEmitters { get; set; }

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
            output.WriteValueU32(this.Unknown3);
            output.WriteValueU32(this.Unknown4);
            output.WriteValueU32(this.Unknown5);
            this.Rotation.Serialize(output);
            this.Translation.Serialize(output);
            output.WriteValueU32(this.NumEmitters);
        }

        public override void Deserialize(Stream input)
        {
            this.Version = input.ReadValueU32();
            this.Name = input.ReadStringBASCII();
            this.Unknown3 = input.ReadValueU32();
            this.Unknown4 = input.ReadValueU32();
            this.Unknown5 = input.ReadValueU32();
            this.Rotation = new Vector4(input);
            this.Translation = new Vector3(input);
            this.NumEmitters = input.ReadValueU32();
        }
    }
}
