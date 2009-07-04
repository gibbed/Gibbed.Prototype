using System.ComponentModel;
using System.IO;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00019003)]
    public class TextureSource : BaseNode
    {
        [Category("Image")]
        public string FileName { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteBASCII(this.FileName);
        }

        public override void Deserialize(Stream input)
        {
            this.FileName = input.ReadBASCII();
        }
    }
}
