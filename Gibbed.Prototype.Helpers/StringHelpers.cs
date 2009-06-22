using System;
using System.Text;

namespace Gibbed.Prototype.Helpers
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

            for (int i = 0; i < data.Length; i++)
            {
                seed = (seed << 5) - seed;

                if (data[i] < 0x61)
                {
                    seed += (UInt32)(0x20 + data[i]);
                }
                else
                {
                    seed += data[i];
                }
            }

            return seed;
        }

        public static UInt32 PrototypeHash(this string input)
        {
            return input.PrototypeHash(0);
        }
    }
}
