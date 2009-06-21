using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00019000)]
    public class Image : Node
    {
        public string Name { get; set; }
        public UInt32 Unknown1 { get; set; }

        [Category("Image")]
        public UInt32 Width { get; set; }

        [Category("Image")]
        public UInt32 Height { get; set; }

        [Category("Image")]
        public UInt32 Format { get; set; }

        public UInt32 Unknown5 { get; set; }
        public UInt32 Unknown6 { get; set; }
        public UInt32 Unknown7 { get; set; }
        public UInt32 Unknown8 { get; set; }
        public UInt32 Unknown9 { get; set; }

        public override string ToString()
        {
            return "Image (" + this.Name + ")";
        }

        public override void Serialize(Stream output)
        {
            output.WriteBASCII(this.Name);
            output.WriteU32(this.Unknown1);
            output.WriteU32(this.Width);
            output.WriteU32(this.Height);
            output.WriteU32(this.Format);
            output.WriteU32(this.Unknown5);
            output.WriteU32(this.Unknown6);
            output.WriteU32(this.Unknown7);
            output.WriteU32(this.Unknown8);
            output.WriteU32(this.Unknown9);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadBASCII();
            this.Unknown1 = input.ReadU32();
            this.Width = input.ReadU32();
            this.Height = input.ReadU32();
            this.Format = input.ReadU32();
            this.Unknown5 = input.ReadU32();
            this.Unknown6 = input.ReadU32();
            this.Unknown7 = input.ReadU32();
            this.Unknown8 = input.ReadU32();
            this.Unknown9 = input.ReadU32();
        }

        public override System.Drawing.Image Preview()
        {
            Node node = this.Children.SingleOrDefault(candidate => (candidate is ImageDDS) || (candidate is ImagePNG));
            if (node == null)
            {
                return null;
            }

            return node.Preview();
        }
    }
}
