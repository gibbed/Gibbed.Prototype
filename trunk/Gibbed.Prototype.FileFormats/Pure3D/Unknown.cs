using System;
using System.ComponentModel;
using System.IO;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    public class Unknown : Node
    {
        private UInt32 _TypeId;
        public override uint TypeId { get { return this._TypeId; } }

        public Unknown(UInt32 typeId)
            : base()
        {
            this._TypeId = typeId;
        }

        /*
        [ReadOnly(true)]
        [DisplayName("Type ID")]
        public UInt32 TypeId { get; set; }
        */

        [ReadOnly(true)]
        [Category("Pure3D")]
        public byte[] Data { get; set; }

        public override string ToString()
        {
            return "Unknown (" + this.TypeId.ToString("X8") + ")";
        }

        public override void Serialize(Stream output)
        {
            output.Write(this.Data, 0, this.Data.Length);
        }

        public override void Deserialize(Stream input)
        {
            this.Data = new byte[input.Length];
            input.Read(this.Data, 0, Data.Length);
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
    }
}
