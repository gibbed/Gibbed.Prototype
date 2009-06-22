using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
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

        private Node GetSubImageNode()
        {
            return this.Children.SingleOrDefault(candidate => (candidate is ImageDDS) || (candidate is ImagePNG));
        }

        public override System.Drawing.Image Preview()
        {
            Node node = this.GetSubImageNode();
            if (node == null)
            {
                return null;
            }

            return node.Preview();
        }

        public override bool Exportable
        {
            get
            {
                Node node = this.GetSubImageNode();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Export(Stream output)
        {
            Node node = this.GetSubImageNode();
            if (node == null)
            {
                throw new InvalidOperationException();
            }
            node.Export(output);
        }

        public override bool Importable
        {
            get
            {
                Node node = this.GetSubImageNode();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Import(Stream input)
        {
            Node node = this.GetSubImageNode();
            if (node == null)
            {
                throw new InvalidOperationException();
            }
            node.Import(input);
        }
    }
}
