using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00011018)]
    public class U00011018 : BaseNode
    {
        public string Name { get; set; }
        public float ValueA { get; set; }
        public float ValueB { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteBASCII(this.Name);
            output.WriteValueF32(this.ValueA);
            output.WriteValueF32(this.ValueB);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadBASCII();
            this.ValueA = input.ReadValueF32();
            this.ValueB = input.ReadValueF32();
        }
    }
}
