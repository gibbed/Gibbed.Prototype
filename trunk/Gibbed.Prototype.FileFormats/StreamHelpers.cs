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

using System.IO;
using System.Text;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats
{
    public static class StreamHelpers
    {
        public static string ReadStringAlignedASCII(this Stream stream)
        {
            int length = stream.ReadValueS32();

            if (length == 0)
            {
                return null;
            }

            var data = new byte[length];
            stream.ReadAligned(data, 0, data.Length, 4);
            return Encoding.ASCII.GetString(data);
        }

        public static string ReadStringBASCII(this Stream stream)
        {
            byte size = stream.ReadValueU8();

            if (size == 0)
            {
                return "";
            }

            var data = new byte[size];
            stream.Read(data, 0, data.Length);

            int length = data.Length;
            for (; length > 0; length--)
            {
                if (data[length - 1] != 0)
                {
                    break;
                }
            }

            return Encoding.ASCII.GetString(data, 0, length);
        }

        public static void WriteStringBASCII(this Stream stream, string value)
        {
            if (value.Length == 0)
            {
                stream.WriteValueU8(0);
                return;
            }

            if (value.Length > 255)
            {
                value = value.Substring(0, 255);
            }

            int padding = value.Length % 4;
            if (padding > 0)
            {
                padding = 4 - padding;
                if (value.Length + padding >= 255)
                {
                    padding = 255 - value.Length;
                }
            }

            stream.WriteValueU8((byte)(value.Length + padding));
            byte[] data = Encoding.ASCII.GetBytes(value);
            stream.Write(data, 0, data.Length);

            if (padding > 0)
            {
                var junk = new byte[padding];
                stream.Write(junk, 0, junk.Length);
            }
        }
    }
}
