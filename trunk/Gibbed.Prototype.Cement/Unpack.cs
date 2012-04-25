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
using Gibbed.Prototype.FileFormats;
using Ionic.Zlib;
using NConsoler;

namespace Gibbed.Prototype.Cement
{
    internal partial class Program
    {
        [Action(Description = "Unpack a cement (*.rcf) file")]
        public static void Unpack(
            [Required(Description = "input cement file")] string inputPath,
            [Required(Description = "output directory")] string outputPath,
            [Optional(false, "rz", Description = "unpack compressed data (*.rz)")] bool unpackRz,
            [Optional(false, "ow", Description = "overwrite existing files")] bool overwrite)
        {
            Stream input = File.OpenRead(inputPath);
            Directory.CreateDirectory(outputPath);

            var cement = new CementFile();
            cement.Deserialize(input);

            Console.WriteLine("{0} files in cement file.", cement.Entries.Count);

            long counter = 0;
            long skipped = 0;
            long totalCount = cement.Entries.Count;
            foreach (CementEntry entry in cement.Entries)
            {
                counter++;

                CementMetadata metadata = cement.GetMetadata(entry.NameHash);

                bool unpacking = false;
                string partPath;
                if (metadata == null)
                {
                    partPath = Path.Combine("__UNKNOWN", entry.NameHash.ToString("X8"));
                }
                else
                {
                    partPath = metadata.Name;
                    if (partPath.StartsWith("\\") == true)
                    {
                        partPath = partPath.Substring(1);
                    }

                    if (Path.GetExtension(partPath) == ".rz" && unpackRz == true)
                    {
                        unpacking = true;
                        partPath = Path.ChangeExtension(partPath, null);
                    }
                }

                Directory.CreateDirectory(Path.Combine(outputPath, Path.GetDirectoryName(partPath)));
                string entryPath = Path.Combine(outputPath, partPath);

                if (overwrite == false && File.Exists(entryPath) == true)
                {
                    Console.WriteLine("{1:D4}/{2:D4} !! {0}", partPath, counter, totalCount);
                    skipped++;
                    continue;
                }

                Console.WriteLine("{1:D4}/{2:D4} => {0}", partPath, counter, totalCount);

                input.Seek(entry.Offset, SeekOrigin.Begin);

                Stream output = File.Open(entryPath, FileMode.Create, FileAccess.Write, FileShare.Read);

                if (unpacking == true)
                {
                    if (input.ReadString(4, true, Encoding.ASCII) != "RZ")
                    {
                        unpacking = false;
                    }
                    else
                    {
                        UInt32 unknown1 = input.ReadValueU32();
                        int uncompressedSize = input.ReadValueS32();
                        UInt32 unknown2 = input.ReadValueU32();

                        if (unknown1 != 0 || unknown2 != 0)
                        {
                            throw new Exception();
                        }

                        var zlib = new ZlibStream(input, CompressionMode.Decompress, true);
                        int left = uncompressedSize;
                        var block = new byte[4096];
                        while (left > 0)
                        {
                            int read = zlib.Read(block, 0, Math.Min(block.Length, left));
                            if (read == 0)
                            {
                                break;
                            }

                            output.Write(block, 0, read);
                            left -= read;
                        }
                        zlib.Close();
                    }
                }

                if (unpacking == false)
                {
                    long left = entry.Size;
                    var data = new byte[4096];
                    while (left > 0)
                    {
                        var block = (int)(Math.Min(left, 4096));
                        input.Read(data, 0, block);
                        output.Write(data, 0, block);
                        left -= block;
                    }
                }

                output.Close();
            }

            input.Close();

            if (skipped > 0)
            {
                Console.WriteLine("{0} files not overwritten.", skipped);
            }
        }
    }
}
