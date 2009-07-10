﻿using System;
using System.IO;
using System.Linq;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x07F00000)]
    public class MetaObjectDefinition : BaseNode
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string TypeName { get; set; }
        public UInt16 Unknown4 { get; set; }
        public UInt16 Unknown5 { get; set; }
        public UInt32 Unknown6 { get; set; }

        public override string ToString()
        {
            if (this.LongName == null || this.LongName.Length == 0)
            {
                return base.ToString();
            }

            return base.ToString() + " (" + this.LongName + ")";
        }

        public override void Serialize(Stream output)
        {
            output.WriteStringBASCII(this.LongName);
            output.WriteStringBASCII(this.ShortName);
            output.WriteStringBASCII(this.TypeName);
            output.WriteValueU16(this.Unknown4);
            output.WriteValueU16(this.Unknown5);
            output.WriteValueU32(this.Unknown6);
        }

        public override void Deserialize(Stream input)
        {
            this.LongName = input.ReadStringBASCII();
            this.ShortName = input.ReadStringBASCII();
            this.TypeName = input.ReadStringBASCII();
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
