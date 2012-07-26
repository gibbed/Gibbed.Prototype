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
using System.Collections.Generic;
using System.IO;
using Gibbed.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using NDesk.Options;

namespace Gibbed.Prototype.Uncompress
{
    internal class Program
    {
        private static string GetExecutableName()
        {
            return Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
        }

        public static void Main(string[] args)
        {
            bool showHelp = false;
            bool overwriteFiles = false;
            bool verbose = false;

            var options = new OptionSet()
            {
                {
                    "o|overwrite",
                    "overwrite existing files",
                    v => overwriteFiles = v != null
                    },
                {
                    "v|verbose",
                    "be verbose",
                    v => verbose = v != null
                    },
                {
                    "h|help",
                    "show this message and exit",
                    v => showHelp = v != null
                    },
            };

            List<string> extras;

            try
            {
                extras = options.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("{0}: ", GetExecutableName());
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `{0} --help' for more information.", GetExecutableName());
                return;
            }

            if (extras.Count < 1 || extras.Count > 2 || showHelp == true)
            {
                Console.WriteLine("Usage: {0} [OPTIONS]+ input_rz [output_bin]", GetExecutableName());
                Console.WriteLine();
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            var inputPath = Path.GetFullPath(extras[0]);

            string outputPath;
            if (extras.Count > 1)
            {
                outputPath = extras[1];
            }
            else
            {
                var extension = Path.GetExtension(inputPath);
                if (extension != null && extension.ToLowerInvariant() == ".rz")
                {
                    outputPath = Path.ChangeExtension(inputPath, null);
                }
                else
                {
                    outputPath = inputPath + ".unc";
                }
            }

            if (overwriteFiles == false &&
                File.Exists(outputPath) == true)
            {
                Console.WriteLine("error: '{0}' exists");
                return;
            }

            using (var temp = new MemoryStream())
            {
                using (var input = File.OpenRead(inputPath))
                {
                    var magic = input.ReadValueU32(Endian.Big);
                    if (magic != 0x525A0000) // 'RZ\0\0'
                    {
                        Console.WriteLine("error: '{0}' is not an RZ file");
                        return;
                    }

                    var unknown1 = input.ReadValueU32(Endian.Little);
                    var uncompressedSize = input.ReadValueS32(Endian.Little);
                    var unknown2 = input.ReadValueU32(Endian.Little);

                    if (unknown1 != 0 || unknown2 != 0)
                    {
                        throw new FormatException();
                    }

                    var zlib = new InflaterInputStream(input);
                    temp.WriteFromStream(zlib, uncompressedSize);
                }

                using (var output = File.Create(outputPath))
                {
                    temp.Position = 0;
                    output.WriteFromStream(temp, temp.Length);
                }
            }
        }
    }
}
