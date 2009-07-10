using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00019001)]
    public class TexturePNG : BaseNode
    {
        public string Name { get; set; }
        public UInt32 Unknown1 { get; set; }

        [Category("Image")]
        public UInt32 Width { get; set; }

        [Category("Image")]
        public UInt32 Height { get; set; }

        public UInt32 Bpp { get; set; }
        public UInt32 Palettized { get; set; }
        public UInt32 HasAlpha { get; set; }
        public UInt32 Format { get; set; }

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
            output.WriteValueU32(this.Bpp);
            output.WriteValueU32(this.Palettized);
            output.WriteValueU32(this.HasAlpha);
            output.WriteValueU32(this.Format);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Unknown1 = input.ReadValueU32();
            this.Width = input.ReadValueU32();
            this.Height = input.ReadValueU32();
            this.Bpp = input.ReadValueU32();
            this.Palettized = input.ReadValueU32();
            this.HasAlpha = input.ReadValueU32();
            this.Format = input.ReadValueU32();
        }

        private TextureData GetSubImageDataNode()
        {
            return (TextureData)this.Children.SingleOrDefault(candidate => candidate is TextureData);
        }

        public override object Preview()
        {
            TextureData data = this.GetSubImageDataNode();
            if (data == null)
            {
                return null;
            }

            MemoryStream memory = new MemoryStream();
            memory.Write(data.Data, 0, data.Data.Length);
            memory.Seek(0, SeekOrigin.Begin);

            return Bitmap.FromStream(memory);
        }

        public override bool Exportable
        {
            get
            {
                BaseNode node = this.GetSubImageDataNode();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Export(Stream output)
        {
            BaseNode node = this.GetSubImageDataNode();
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
                BaseNode node = this.GetSubImageDataNode();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Import(Stream input)
        {
            BaseNode node = this.GetSubImageDataNode();
            if (node == null)
            {
                throw new InvalidOperationException();
            }
            node.Import(input);
        }
    }
}
