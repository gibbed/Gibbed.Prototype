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
using System.IO;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x07F00000)]
    public class MetaObjectDefinition : BaseNode
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string TypeName { get; set; }
        public ushort Unknown4 { get; set; }
        public ushort Unknown5 { get; set; }
        public uint Unknown6 { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.LongName) == true)
            {
                return base.ToString();
            }

            return base.ToString() + " (" + this.LongName + ")";
        }

        public override void Serialize(Stream output)
        {
            output.WriteStringAlignedU8(this.LongName);
            output.WriteStringAlignedU8(this.ShortName);
            output.WriteStringAlignedU8(this.TypeName);
            output.WriteValueU16(this.Unknown4);
            output.WriteValueU16(this.Unknown5);
            output.WriteValueU32(this.Unknown6);
        }

        public override void Deserialize(Stream input)
        {
            this.LongName = input.ReadStringAlignedU8();
            this.ShortName = input.ReadStringAlignedU8();
            this.TypeName = input.ReadStringAlignedU8();
            this.Unknown4 = input.ReadValueU16();
            this.Unknown5 = input.ReadValueU16();
            this.Unknown6 = input.ReadValueU32();
        }

        public override bool Exportable
        {
            get
            {
                BaseNode node = this.GetChildNode<MetaObjectData>();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Export(Stream output)
        {
            BaseNode node = this.GetChildNode<MetaObjectData>();
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
                BaseNode node = this.GetChildNode<MetaObjectData>();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Import(Stream input)
        {
            BaseNode node = this.GetChildNode<MetaObjectData>();
            if (node == null)
            {
                throw new InvalidOperationException();
            }
            node.Import(input);
        }
    }
}
