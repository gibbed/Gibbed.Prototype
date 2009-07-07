﻿using System;
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

        public UInt32 Unknown4 { get; set; }
        public UInt32 Unknown5 { get; set; }
        public UInt32 Unknown6 { get; set; }
        public UInt32 Unknown7 { get; set; }

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
            output.WriteValueU32(this.Unknown4);
            output.WriteValueU32(this.Unknown5);
            output.WriteValueU32(this.Unknown6);
            output.WriteValueU32(this.Unknown7);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Unknown1 = input.ReadValueU32();
            this.Width = input.ReadValueU32();
            this.Height = input.ReadValueU32();
            this.Unknown4 = input.ReadValueU32();
            this.Unknown5 = input.ReadValueU32();
            this.Unknown6 = input.ReadValueU32();
            this.Unknown7 = input.ReadValueU32();
        }

        private TextureData GetSubImageDataNode()
        {
            return (TextureData)this.Children.SingleOrDefault(candidate => candidate is TextureData);
        }

        public override System.Drawing.Image Preview()
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