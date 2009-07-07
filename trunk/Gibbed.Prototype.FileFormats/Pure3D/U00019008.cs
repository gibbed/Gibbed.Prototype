using System.IO;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    //[KnownType(0x00019008)]
    public class U00019008 : BaseNode
    {
        public string Name { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteStringBASCII(this.Name);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
        }
    }
}
