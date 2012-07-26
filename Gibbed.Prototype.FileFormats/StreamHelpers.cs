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

namespace Gibbed.Prototype.FileFormats
{
    public static class StreamHelpers
    {
        public static string ReadStringU32(this Stream stream, Endian endian)
        {
            var size = stream.ReadValueU32(endian);
            if (size == 0)
            {
                return "";
            }

            return stream.ReadString(size, true, Encoding.ASCII);
        }

        public static void WriteStringU32(this Stream stream, string value, Endian endian)
        {
            if (string.IsNullOrEmpty(value) == true)
            {
                stream.WriteValueU32(0, endian);
                return;
            }

            var byteCount = Encoding.ASCII.GetByteCount(value);
            stream.WriteValueS32(byteCount + 1, endian);
            stream.WriteStringZ(value, Encoding.ASCII);
        }

        public static string ReadStringAlignedU8(this Stream stream)
        {
            var size = stream.ReadValueU8();
            if (size == 0)
            {
                return "";
            }

            return stream.ReadString(size, true, Encoding.ASCII);
        }

        public static void WriteStringAlignedU8(this Stream stream, string value)
        {
            if (string.IsNullOrEmpty(value) == true)
            {
                stream.WriteValueU8(0);
                return;
            }

            var byteCount = Encoding.ASCII.GetByteCount(value);
            
            if (byteCount > 255)
            {
                throw new ArgumentException("value is too long", "value");
            }

            int padding = 4 - (byteCount % 4);
            if (padding > 0)
            {
                if (byteCount + padding > 255)
                {
                    throw new ArgumentException("value is too long (due to padding)", "value");
                }
            }

            stream.WriteValueU8((byte)(byteCount + padding));

            var data = Encoding.ASCII.GetBytes(value);
            Array.Resize(ref data, byteCount + padding);
            stream.Write(data, 0, data.Length);
        }
    }
}
