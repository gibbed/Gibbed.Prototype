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
using System.IO;
using System.Linq;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00019000)]
    public class Texture : BaseNode
    {
        public string Name { get; set; }
        public uint Version { get; set; }

        [Category("Image")]
        public uint Width { get; set; }

        [Category("Image")]
        public uint Height { get; set; }

        [Category("Image")]
        public uint Bpp { get; set; }

        public uint AlphaDepth { get; set; }
        public uint NumMipMaps { get; set; }
        public uint TextureType { get; set; }
        public uint Usage { get; set; }
        public uint Priority { get; set; }

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
