using System;
using System.IO;
using System.Text;

namespace Gibbed.Prototype.FileFormats
{
    public class FourCC
    {
        private byte[] Chars = new byte[] { 0, 0, 0, 0 };

        public FourCC()
        {
        }

        public FourCC(Stream input)
        {
            this.Deserialize(input);
        }

        public override string ToString()
        {
            return Encoding.ASCII.GetString(Chars).TrimEnd('\0');
        }

        public static implicit operator FourCC(string s)
        {
            if (s.Length != 4)
            {
                throw new InvalidCastException();
            }
            
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            return new FourCC() { Chars = bytes };
        }

        public override int GetHashCode()
        {
            return this.Chars.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            FourCC a = obj as FourCC;

            if (a != null)
            {
                return
                    a.Chars[0] == Chars[0] &&
                    a.Chars[1] == Chars[1] &&
                    a.Chars[2] == Chars[2] &&
                    a.Chars[3] == Chars[3];
            }

            return false;
        }

        public void Serialize(Stream output)
        {
            output.Write(Chars, 0, 4);
        }

        public void Deserialize(Stream input)
        {
            input.Read(Chars, 0, 4);
        }
    }
}
