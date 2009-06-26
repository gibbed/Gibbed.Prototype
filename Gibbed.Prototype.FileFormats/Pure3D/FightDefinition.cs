using System;
using System.IO;
using System.Linq;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x20000701)]
    public class FightDefinition : Node
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
            output.WriteBASCII(this.Name);
            output.WriteU16(this.Unknown2);
            output.WriteBASCII(this.Context);
            output.WriteU32(this.Unknown4);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadBASCII();
            this.Unknown2 = input.ReadU16();
            this.Context = input.ReadBASCII();
            this.Unknown4 = input.ReadU32();
        }

        private FightData GetChildData()
        {
            return (FightData)this.Children.SingleOrDefault(candidate => candidate is FightData);
        }

        public override bool Exportable
        {
            get
            {
                Node node = this.GetChildData();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Export(Stream output)
        {
            Node node = this.GetChildData();
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
                Node node = this.GetChildData();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Import(Stream input)
        {
            Node node = this.GetChildData();
            if (node == null)
            {
                throw new InvalidOperationException();
            }
            node.Import(input);
        }
    }
}
