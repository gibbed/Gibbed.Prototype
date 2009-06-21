using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Gibbed.Helpers;
using System.Text;

namespace Gibbed.Prototype.Helpers
{
    public static partial class StreamHelpers
    {
        public static string ReadBASCII(this Stream stream)
        {
            byte size = stream.ReadU8();

            if (size == 0)
            {
                return "";
            }

            byte[] data = new byte[size];
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

        public static void WriteBASCII(this Stream stream, string value)
        {
            if (value.Length == 0)
            {
                stream.WriteU8(0);
                return;
            }
            else if (value.Length > 254)
            {
                value = value.Substring(0, 254);
            }

            stream.WriteU8((byte)(value.Length + 1));
            byte[] data = Encoding.ASCII.GetBytes(value);
            stream.Write(data, 0, data.Length);
            stream.WriteByte(0);
        }
    }

}
