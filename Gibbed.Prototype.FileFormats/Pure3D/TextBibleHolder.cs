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
using System.Collections.Generic;
using System.IO;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00018202)]
    public class TextBibleHolder : BaseNode
    {
        public string Language { get; set; }
        public uint Version { get; set; }
        public List<string> Keys { get; set; }
        public List<uint> StringStarts { get; set; }
        public List<uint> StringStops { get; set; }

        public override void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input)
        {
            this.Language = input.ReadStringAlignedU8();
            this.Version = input.ReadValueU32();

            var count = input.ReadValueU32();

            this.Keys = new List<string>();
            for (uint i = 0; i < count; i++)
            {
                this.Keys.Add(input.ReadStringAlignedU8());
            }

            this.StringStarts = new List<UInt32>();
            for (uint i = 0; i < count; i++)
            {
                this.StringStarts.Add(input.ReadValueU32());
            }

            this.StringStops = new List<UInt32>();
            for (uint i = 0; i < count; i++)
            {
                this.StringStops.Add(input.ReadValueU32());
            }
        }
    }
}
