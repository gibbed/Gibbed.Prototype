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
        public UInt32 Unknown1 { get; set; }
        public string Name { get; set; }
        public List<float> Unknown3 { get; set; }
        public List<UInt32> Unknown4 { get; set; }

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
            this.Unknown1 = input.ReadValueU32();
            this.Name = input.ReadBASCII();
            
            uint count = input.ReadValueU32();

            this.Unknown3 = new List<float>();
            for (uint i = 0; i < count; i++)
            {
                this.Unknown3.Add(input.ReadValueF32());
            }

            this.Unknown4 = new List<UInt32>();
            for (uint i = 0; i < count; i++)
            {
                this.Unknown4.Add(input.ReadValueU32());
            }
        }
    }
}
