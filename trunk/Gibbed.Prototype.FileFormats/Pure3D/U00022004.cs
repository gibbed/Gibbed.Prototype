using System;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    //[KnownType(0x00022004)]
    public class U00022004 : BaseNode
    {
        public float Unknown1;

        public override void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadF32();

            int count = input.ReadS32();
            for (int i = 0; i < count; i++)
            {
                input.ReadU32();
                input.ReadU32();
                input.ReadU32();
                input.ReadU32();
                input.ReadU32();
            }
        }
    }
}
