using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00019000)]
    public class Texture : BaseNode
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
            if (this.Name == null || this.Name.Length == 0)
            {
                return base.ToString();
            }

            return base.ToString() + " (" + this.Name + ")";
        }

        public override void Serialize(Stream output)
        {
            output.WriteStringBASCII(this.Name);
            output.WriteValueU32(this.Unknown1);
            output.WriteValueU32(this.Width);
            output.WriteValueU32(this.Height);
            output.WriteValueU32(this.Format);
            output.WriteValueU32(this.Unknown5);
            output.WriteValueU32(this.Unknown6);
            output.WriteValueU32(this.Unknown7);
            output.WriteValueU32(this.Unknown8);
            output.WriteValueU32(this.Unknown9);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Unknown1 = input.ReadValueU32();
            this.Width = input.ReadValueU32();
            this.Height = input.ReadValueU32();
            this.Format = input.ReadValueU32();
            this.Unknown5 = input.ReadValueU32();
            this.Unknown6 = input.ReadValueU32();
            this.Unknown7 = input.ReadValueU32();
            this.Unknown8 = input.ReadValueU32();
            this.Unknown9 = input.ReadValueU32();
        }

        private BaseNode GetSubImageNode()
        {
            return this.Children.SingleOrDefault(candidate => (candidate is TextureDDS) || (candidate is TexturePNG));
        }

        public override System.Drawing.Image Preview()
        {
            BaseNode node = this.GetSubImageNode();
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
                BaseNode node = this.GetSubImageNode();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Export(Stream output)
        {
            BaseNode node = this.GetSubImageNode();
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
                BaseNode node = this.GetSubImageNode();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Import(Stream input)
        {
            BaseNode node = this.GetSubImageNode();
            if (node == null)
            {
                throw new InvalidOperationException();
            }
            node.Import(input);
        }
    }
}
