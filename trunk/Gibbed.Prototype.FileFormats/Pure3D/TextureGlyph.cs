using System;
using System.ComponentModel;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TextureGlyph
    {
        public UInt32 TextureNum { get; set; }

        public Vector2 BottomLeft { get; set; }
        public Vector2 TopRight { get; set; }

        public float LeftBearing { get; set; }
        public float RightBearing { get; set; }
        public float Width { get; set; }
        public float Advance { get; set; }

        public UInt32 Code { get; set; }

        public TextureGlyph()
        {
        }

        public TextureGlyph(Stream input)
        {
            this.Deserialize(input);
        }

        public void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input)
        {
            this.TextureNum = input.ReadValueU32();

            this.BottomLeft = new Vector2(input);
            this.TopRight = new Vector2(input);

            this.LeftBearing = input.ReadValueF32();
            this.RightBearing = input.ReadValueF32();
            this.Width = input.ReadValueF32();
            this.Advance = input.ReadValueF32();

            this.Code = input.ReadValueU32();
        }
    }
}
