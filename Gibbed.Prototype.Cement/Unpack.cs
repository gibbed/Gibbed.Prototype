using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.FileFormats;
using Ionic.Zlib;
using NConsoler;

namespace Gibbed.Prototype.Cement
{
    internal partial class Program
    {
        [Action(Description = "Unpack a cement (*.rcf) file")]
        public static void Unpack(
            [Required(Description = "input cement file")]
            string inputPath,
            [Required(Description = "output directory")]
            string outputPath,
            [Optional(false, "rz", Description = "unpack compressed data (*.rz)")]
            bool unpackRz)
        {
            Stream input = File.OpenRead(inputPath);
            Directory.CreateDirectory(outputPath);

            CementFile cement = new CementFile();
            cement.Deserialize(input);

            Console.WriteLine("{0} files in cement file.", cement.Entries.Count);

            foreach (CementEntry entry in cement.Entries)
            {
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

                Console.WriteLine(partPath);

                Directory.CreateDirectory(Path.Combine(outputPath, Path.GetDirectoryName(partPath)));
                string entryPath = Path.Combine(outputPath, partPath);

                input.Seek(entry.Offset, SeekOrigin.Begin);

                Stream output = File.Open(entryPath, FileMode.Create, FileAccess.Write, FileShare.Read);

                if (unpacking == true)
                {
                    if (input.ReadASCII(4, true) != "RZ")
                    {
                        unpacking = false;
                    }
                    else
                    {
                        UInt32 unknown1 = input.ReadU32();
                        int uncompressedSize = input.ReadS32();
                        UInt32 unknown2 = input.ReadU32();

                        if (unknown1 != 0 || unknown2 != 0)
                        {
                            throw new Exception();
                        }

                        ZlibStream zlib = new ZlibStream(input, CompressionMode.Decompress, true);
                        int left = uncompressedSize;
                        byte[] block = new byte[4096];
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
                    byte[] data = new byte[4096];
                    while (left > 0)
                    {
                        int block = (int)(Math.Min(left, 4096));
                        input.Read(data, 0, block);
                        output.Write(data, 0, block);
                        left -= block;
                    }
                }

                output.Close();
            }

            input.Close();
        }
    }
}
