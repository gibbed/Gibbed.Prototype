using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00011018)]
    public class U00011018 : BaseNode
    {
        public string Name { get; set; }
        public Vector2 Value { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteStringBASCII(this.Name);
            this.Value.Serialize(output);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Value = new Vector2(input);
        }
    }
}
