using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    public class Unknown : Node
    {
        [ReadOnly(true)]
        [DisplayName("Type ID")]
        public UInt32 TypeId { get; set; }

        [ReadOnly(true)]
        public byte[] Data { get; set; }

        public Unknown(UInt32 typeId)
        {
            this.TypeId = typeId;
        }

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
    }
}
