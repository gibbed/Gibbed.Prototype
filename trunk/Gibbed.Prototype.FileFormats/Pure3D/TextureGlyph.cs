/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.ComponentModel;
using System.IO;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TextureGlyph
    {
        public uint TextureNum { get; set; }

        public Vector2 BottomLeft { get; set; }
        public Vector2 TopRight { get; set; }

        public float LeftBearing { get; set; }
        public float RightBearing { get; set; }
        public float Width { get; set; }
        public float Advance { get; set; }

        public uint Code { get; set; }

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
