﻿using System.IO;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00011016)]
    public class U00011016 : BaseNode
    {
        public string Unknown1 { get; set; }
        public string Unknown2 { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteStringBASCII(this.Unknown1);
            output.WriteStringBASCII(this.Unknown2);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadStringBASCII();
            this.Unknown2 = input.ReadStringBASCII();
        }
    }
}
