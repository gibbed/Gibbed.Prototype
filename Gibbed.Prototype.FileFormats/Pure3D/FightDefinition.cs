using System;
using System.IO;
using System.Linq;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x20000701)]
    public class FightDefinition : BaseNode
    {
        public string Name { get; set; }
        public UInt16 Unknown2 { get; set; }
        public string Context { get; set; }
        public UInt32 Unknown4 { get; set; }

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
            output.WriteValueU16(this.Unknown2);
            output.WriteStringBASCII(this.Context);
            output.WriteValueU32(this.Unknown4);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Unknown2 = input.ReadValueU16();
            this.Context = input.ReadStringBASCII();
            this.Unknown4 = input.ReadValueU32();
        }

        public override bool Exportable
        {
            get
            {
                BaseNode node = this.GetChildNode<FightData>();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Export(Stream output)
        {
            BaseNode node = this.GetChildNode<FightData>();
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
                BaseNode node = this.GetChildNode<FightData>();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Import(Stream input)
        {
            BaseNode node = this.GetChildNode<FightData>();
            if (node == null)
            {
                throw new InvalidOperationException();
            }
            node.Import(input);
        }
    }
}
