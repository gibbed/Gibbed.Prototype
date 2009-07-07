using System.ComponentModel;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x20000702)]
    public class FightData : BaseNode
    {
        [ReadOnly(true)]
        public byte[] Data { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteValueS32(this.Data.Length);
            output.Write(this.Data, 0, this.Data.Length);
        }

        public override void Deserialize(Stream input)
        {
            int length = input.ReadValueS32();
            this.Data = new byte[length];
            input.Read(this.Data, 0, this.Data.Length);
        }

        public override bool Exportable
        {
            get
            {
                return this.Data != null && this.Data.Length > 0;
            }
        }

        public override void Export(Stream output)
        {
            output.Write(this.Data, 0, this.Data.Length);
        }

        public override bool Importable
        {
            get
            {
                return true;
            }
        }

        public override void Import(Stream input)
        {
            this.Data = new byte[input.Length];
            input.Read(this.Data, 0, this.Data.Length);
        }
    }
}
