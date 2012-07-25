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

namespace Gibbed.Prototype.FileFormats
{
    // ReSharper disable InconsistentNaming
    public class FourCC
        // ReSharper restore InconsistentNaming
    {
        private readonly byte[] _Chars = new byte[] { 0, 0, 0, 0 };

        public FourCC()
        {
        }

        public FourCC(byte[] chars)
        {
            if (chars == null)
            {
                throw new ArgumentNullException("chars");
            }

            if (chars.Length != 4)
            {
                throw new ArgumentException("chars must have a length of 4");
            }

            Array.Copy(chars, this._Chars, 4);
        }

        public FourCC(Stream input)
        {
            this.Deserialize(input);
        }

        public override string ToString()
        {
            return Encoding.ASCII.GetString(this._Chars).TrimEnd('\0');
        }

        public static implicit operator FourCC(string s)
        {
            var chars = Encoding.ASCII.GetBytes(s);
            if (chars.Length != 4)
            {
                throw new InvalidCastException();
            }

            return new FourCC(chars);
        }

        public override int GetHashCode()
        {
            return this._Chars.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var a = obj as FourCC;

            if (a != null)
            {
                return a._Chars[0] == this._Chars[0] &&
                       a._Chars[1] == this._Chars[1] &&
                       a._Chars[2] == this._Chars[2] &&
                       a._Chars[3] == this._Chars[3];
            }

            return false;
        }

        public void Serialize(Stream output)
        {
            output.Write(this._Chars, 0, 4);
        }

        public void Deserialize(Stream input)
        {
            input.Read(this._Chars, 0, 4);
        }
    }
}
