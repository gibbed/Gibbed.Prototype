﻿using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00011015)]
    public class NewShader : Node
    {
        public string Name { get; set; }
        public UInt32 Unknown2 { get; set; }
        public string Unknown3 { get; set; }
        public UInt32 Unknown4 { get; set; }

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
            output.WriteBASCII(this.Name);
            output.WriteU32(this.Unknown2);
            output.WriteBASCII(this.Unknown3);
            output.WriteU32(this.Unknown4);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadBASCII();
            this.Unknown2 = input.ReadU32();
            this.Unknown3 = input.ReadBASCII();
            this.Unknown4 = input.ReadU32();
        }
    }
}