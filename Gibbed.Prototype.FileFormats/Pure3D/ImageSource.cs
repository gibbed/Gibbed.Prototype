using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00019003)]
    public class ImageSource : Node
    {
        public string FileName { get; set; }

        public override string ToString()
        {
            return "Image Source";
        }

        public override void Serialize(Stream output)
        {
            output.WriteBASCII(this.FileName);
        }

        public override void Deserialize(Stream input)
        {
            this.FileName = input.ReadBASCII();
        }
    }
}
