using System;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    //[KnownType(0x00022001)]
    public class U00022001 : BaseNode
    {
        public override void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input)
        {
            int count = input.ReadValueS32();

            for (int i = 0; i < count; i++)
            {
                input.ReadValueU32();
                input.ReadValueU32();
                input.ReadValueU32();
                input.ReadValueU32();
                input.ReadValueU32();
                input.ReadValueU32();
                input.ReadValueU32();
                input.ReadValueU32();
                input.ReadValueU32();
                input.ReadValueU32();
            }
        }
    }
}
