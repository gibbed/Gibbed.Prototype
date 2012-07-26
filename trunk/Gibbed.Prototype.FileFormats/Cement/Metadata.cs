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
using System.IO;
using System.Text;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats.Cement
{
    public class Metadata
    {
        public uint TypeHash;
        public uint Alignment;
        public uint Unknown2;
        public string Name;
        public readonly byte[] Unknown3 = new byte[3];

        public int ByteSize
        {
            get
            {
                return
                    4 + // TypeHash
                    4 + // Alignment
                    4 + // Unknown2
                    4 + Encoding.ASCII.GetByteCount(this.Name) + 1 + // Name
                    3; // Unknown3
            }
        }

        public void Serialize(Stream output, Endian endian)
        {
            output.WriteValueU32(this.TypeHash, Endian.Little);
            output.WriteValueU32(this.Alignment, Endian.Little);
            output.WriteValueU32(0, Endian.Little); // unknown2
            output.WriteStringU32(this.Name, endian);
            output.WriteBytes(this.Unknown3);
        }

        public void Deserialize(Stream input, Endian endian)
        {
            this.TypeHash = input.ReadValueU32(Endian.Little);
            this.Alignment = input.ReadValueU32(Endian.Little);
            
            var unknown2 = input.ReadValueU32(Endian.Little);
            if (unknown2 != 0)
            {
                throw new FormatException();
            }

            this.Name = input.ReadStringU32(Endian.Little);
            
            if (input.Read(this.Unknown3, 0, 3) != 3)
            {
                throw new EndOfStreamException();
            }

            if (this.Unknown3[0] != 0 ||
                this.Unknown3[1] != 0 ||
                this.Unknown3[2] != 0)
            {
                throw new FormatException();
            }
        }
    }
}
