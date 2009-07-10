using System;
using System.Collections.Generic;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00021000)]
    public class Expression : BaseNode
    {
        public UInt32 Version { get; set; }
        public string Name { get; set; }
        public UInt32 Count { get; set; }
        public float[] Unknown3 { get; set; }
        public UInt32[] Unknown4 { get; set; }

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
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input)
        {
            this.Version = input.ReadValueU32();
            this.Name = input.ReadStringBASCII();
            this.Count = input.ReadValueU32();

            this.Unknown3 = new float[this.Count];
            for (uint i = 0; i < this.Count; i++)
            {
                this.Unknown3[i] = input.ReadValueF32();
            }

            this.Unknown4 = new UInt32[this.Count];
            for (uint i = 0; i < this.Count; i++)
            {
                this.Unknown4[i] = input.ReadValueU32();
            }
        }
    }
}
