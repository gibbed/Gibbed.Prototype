using System;
using System.IO;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00022001)]
    public class TextureGlyphList : BaseNode
    {
        public UInt32 NumGlyphs { get; set; }
        public TextureGlyph[] Glyphs { get; set; }

        public override void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input)
        {
            this.NumGlyphs = input.ReadValueU32();
            this.Glyphs = new TextureGlyph[this.NumGlyphs];
            for (int i = 0; i < this.NumGlyphs; i++)
            {
                this.Glyphs[i] = new TextureGlyph(input);
            }
        }
    }
}
