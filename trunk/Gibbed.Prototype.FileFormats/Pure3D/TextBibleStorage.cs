using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00018201)]
    public class TextBibleStorage : BaseNode
    {
        public string Unknown1 { get; set; }
        public UInt32 Unknown2 { get; set; }
        [ReadOnly(true)]
        public byte[] Data { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteStringBASCII(this.Unknown1);
            output.WriteValueU32(this.Unknown2);
            output.WriteValueU32((uint)this.Data.Length);
            output.Write(this.Data, 0, this.Data.Length);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadStringBASCII();
            this.Unknown2 = input.ReadValueU32();
            uint length = input.ReadValueU32();
            this.Data = new byte[length];
            input.Read(this.Data, 0, this.Data.Length);
        }
    }
}
