/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00019001)]
    public class TexturePNG : BaseNode
    {
        public string Name { get; set; }
        public uint Unknown1 { get; set; }

        [Category("Image")]
        public uint Width { get; set; }

        [Category("Image")]
        public uint Height { get; set; }

        public uint Bpp { get; set; }
        public uint Palettized { get; set; }
        public uint HasAlpha { get; set; }
        public uint Format { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Name) == true)
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

        public override object Preview()
        {
            var data = this.GetChildNode<TextureData>();
            if (data == null)
            {
                return null;
            }

            var memory = new MemoryStream();
            memory.Write(data.Data, 0, data.Data.Length);
            memory.Seek(0, SeekOrigin.Begin);

            return Image.FromStream(memory);
        }

        public override bool Exportable
        {
            get
            {
                BaseNode node = this.GetChildNode<TextureData>();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Export(Stream output)
        {
            BaseNode node = this.GetChildNode<TextureData>();
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
                BaseNode node = this.GetChildNode<TextureData>();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Import(Stream input)
        {
            BaseNode node = this.GetChildNode<TextureData>();
            if (node == null)
            {
                throw new InvalidOperationException();
            }
            node.Import(input);
        }
    }
}
