using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00011017)]
    public class U00011017 : BaseNode
    {
        public string Name { get; set; }
        public float Value { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteBASCII(this.Name);
            output.WriteValueF32(this.Value);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadBASCII();
            this.Value = input.ReadValueF32();
        }
    }
}
