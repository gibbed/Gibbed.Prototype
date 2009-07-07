using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    //[KnownType(0xFE000000)]
    public class Registry2 : BaseNode
    {
        public UInt32 Unknown1 { get; set; }
        public string Unknown2 { get; set; }
        public UInt32 Unknown3 { get; set; }
        public string Unknown4 { get; set; }

        public override void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadValueU32();
            this.Unknown2 = input.ReadStringASCII(input.ReadValueU32() + 1, true);
            this.Unknown3 = input.ReadValueU32();
            this.Unknown4 = input.ReadStringASCII(input.ReadValueU32() + 1, true);

            // following data depends on Unknown2 value :|
        }
    }
}
