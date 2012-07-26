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
using System.Globalization;
using System.IO;
using Gibbed.IO;
using Gibbed.Prototype.FileFormats;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using NDesk.Options;

namespace Gibbed.Prototype.Unpack
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
            bool unpackRz = false;
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
                    "rz",
                    "unpack compressed data (*.rz)",
                    v => unpackRz = v != null
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
                Console.WriteLine("Usage: {0} [OPTIONS]+ input_rcf [output_dir]", GetExecutableName());
                Console.WriteLine();
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            var inputPath = Path.GetFullPath(extras[0]);
            var outputPath = extras.Count > 1 ? extras[1] : Path.ChangeExtension(inputPath, null) + "_unpack";

            using (var input = File.OpenRead(inputPath))
            {
                var cement = new CementFile();
                cement.Deserialize(input);

                long current = 0;
                long total = cement.Entries.Count;
                var padding = total.ToString(CultureInfo.InvariantCulture).Length;

                foreach (var entry in cement.Entries)
                {
                    current++;

                    var metadata = cement.GetMetadata(entry.NameHash);

                    bool unpacking = false;
                    string entryName;

                    if (metadata == null)
                    {
                        entryName = Path.Combine("__UNKNOWN", entry.NameHash.ToString("X8"));
                    }
                    else
                    {
                        entryName = metadata.Name;
                        if (entryName.StartsWith("\\") == true)
                        {
                            entryName = entryName.Substring(1);
                        }

                        if (Path.GetExtension(entryName) == ".rz" && unpackRz == true)
                        {
                            unpacking = true;
                            entryName = Path.ChangeExtension(entryName, null);
                        }
                    }

                    var entryPath = Path.Combine(outputPath, entryName);
                    if (overwriteFiles == false &&
                        File.Exists(entryPath) == true)
                    {
                        continue;
                    }

                    if (verbose == true)
                    {
                        Console.WriteLine("[{0}/{1}] {2}",
                                          current.ToString(CultureInfo.InvariantCulture).PadLeft(padding),
                                          total,
                                          entryName);
                    }

                    input.Seek(entry.Offset, SeekOrigin.Begin);

                    var entryParentPath = Path.GetDirectoryName(entryPath);
                    if (string.IsNullOrEmpty(entryParentPath) == false)
                    {
                        Directory.CreateDirectory(entryParentPath);
                    }

                    using (var output = File.Create(entryPath))
                    {
                        if (unpacking == false)
                        {
                            output.WriteFromStream(input, entry.Size);
                        }
                        else
                        {
                            var magic = input.ReadValueU32(Endian.Big);
                            if (magic != 0x525A0000) // 'RZ\0\0'
                            {
                                output.WriteFromStream(input, entry.Size);
                            }
                            else
                            {
                                var unknown1 = input.ReadValueU32(Endian.Little);
                                var uncompressedSize = input.ReadValueS32(Endian.Little);
                                var unknown2 = input.ReadValueU32(Endian.Little);

                                if (unknown1 != 0 || unknown2 != 0)
                                {
                                    throw new FormatException();
                                }

                                var zlib = new InflaterInputStream(input);
                                output.WriteFromStream(zlib, uncompressedSize);
                            }
                        }
                    }
                }
            }
        }
    }
}
