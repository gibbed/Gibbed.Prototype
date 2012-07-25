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
        public byte[] Unknown3;

        public void Serialize(Stream output, Endian endian)
        {
            output.WriteValueU32(this.TypeHash, Endian.Little);
            output.WriteValueU32(this.Alignment, Endian.Little);
            output.WriteValueU32(0, Endian.Little);
            throw new NotImplementedException();
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

            this.Name = input.ReadString(input.ReadValueU32(Endian.Little), true, Encoding.ASCII);
            
            var unknown3 = input.ReadBytes(3);
            if (unknown3[0] != 0 ||
                unknown3[1] != 0 ||
                unknown3[2] != 0)
            {
                throw new FormatException();
            }
        }
    }
}
