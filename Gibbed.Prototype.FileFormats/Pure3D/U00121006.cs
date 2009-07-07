using System;
using System.ComponentModel;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00121006)]
    public class U00121006 : BaseNode
    {
        public UInt32 Unknown1 { get; set; }
        [DisplayName("Count of 00121001's")]
        public UInt32 Unknown2 { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteValueU32(this.Unknown1);
            output.WriteValueU32(this.Unknown2);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadValueU32();
            this.Unknown2 = input.ReadValueU32();
        }
    }
}
