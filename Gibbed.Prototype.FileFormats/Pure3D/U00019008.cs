using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    //[KnownType(0x00019008)]
    public class U00019008 : Node
    {
        public string Name { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteBASCII(this.Name);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadBASCII();
        }
    }
}
