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
using System.Text;
using Enumerable = System.Linq.Enumerable;

namespace Gibbed.Prototype.FileFormats
{
    public static class StringHelpers
    {
        public static UInt32 PrototypeHash(this string input, UInt32 seed)
        {
            if (input.StartsWith("\\") == true)
            {
                input = input.Substring(1);
            }

            byte[] data = Encoding.ASCII.GetBytes(input);

            foreach (byte t in data)
            {
                seed = (seed << 5) - seed;

                if (t < 0x61)
                {
                    seed += (UInt32)(0x20 + t);
                }
                else
                {
                    seed += t;
                }
            }

            return seed;
        }

        public static UInt32 PrototypeHash(this string input)
        {
            return input.PrototypeHash(0);
        }

        public static UInt64 Hash1003F(this string input)
        {
            return Enumerable.Aggregate<char, ulong>(input, 0, (current, t) => (current * 65599) ^ t);
        }
    }
}
