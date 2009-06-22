using System;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    //[KnownType(0x00022006)]
    public class U00022006 : Node
    {
        public UInt32 Unknown;

        public override void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input)
        {
            int count = input.ReadS32();

            for (int i = 0; i < count; i++)
            {
                input.ReadU32();
                input.ReadU32();
            }
        }
    }
}
