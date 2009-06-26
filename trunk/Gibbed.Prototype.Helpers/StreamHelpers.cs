using System.IO;
using System.Text;
using Gibbed.Helpers;

namespace Gibbed.Prototype.Helpers
{
    public static partial class StreamHelpers
    {
        public static string ReadAlignedASCII(this Stream stream)
        {
            int length = stream.ReadS32();
            byte[] data = new byte[length];
            stream.ReadAligned(data, 0, data.Length, 4);
            return Encoding.ASCII.GetString(data);
        }

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
            else if (value.Length > 255)
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

            stream.WriteU8((byte)(value.Length + padding));
            byte[] data = Encoding.ASCII.GetBytes(value);
            stream.Write(data, 0, data.Length);

            if (padding > 0)
            {
                byte[] junk = new byte[padding];
                stream.Write(junk, 0, junk.Length);
            }
        }
    }

}
