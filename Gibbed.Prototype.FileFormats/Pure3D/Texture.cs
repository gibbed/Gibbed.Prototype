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
        public UInt32 Version { get; set; }

        [Category("Image")]
        public UInt32 Width { get; set; }

        [Category("Image")]
        public UInt32 Height { get; set; }

        [Category("Image")]
        public UInt32 Bpp { get; set; }

        public UInt32 AlphaDepth { get; set; }
        public UInt32 NumMipMaps { get; set; }
        public UInt32 TextureType { get; set; }
        public UInt32 Usage { get; set; }
        public UInt32 Priority { get; set; }

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
            output.WriteValueU32(this.Version);
            output.WriteValueU32(this.Width);
            output.WriteValueU32(this.Height);
            output.WriteValueU32(this.Bpp);
            output.WriteValueU32(this.AlphaDepth);
            output.WriteValueU32(this.NumMipMaps);
            output.WriteValueU32(this.TextureType);
            output.WriteValueU32(this.Usage);
            output.WriteValueU32(this.Priority);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Version = input.ReadValueU32();
            this.Width = input.ReadValueU32();
            this.Height = input.ReadValueU32();
            this.Bpp = input.ReadValueU32();
            this.AlphaDepth = input.ReadValueU32();
            this.NumMipMaps = input.ReadValueU32();
            this.TextureType = input.ReadValueU32();
            this.Usage = input.ReadValueU32();
            this.Priority = input.ReadValueU32();
        }

        private BaseNode GetSubImageNode()
        {
            return this.Children.SingleOrDefault(candidate => (candidate is TextureDDS) || (candidate is TexturePNG));
        }

        public override object Preview()
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
