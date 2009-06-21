using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gibbed.Helpers;
using System.IO;
using Gibbed.Prototype.Helpers;
using Gibbed.Prototype.FileFormats;
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
            string outputPath)
        {
            Stream input = File.OpenRead(inputPath);
            Directory.CreateDirectory(outputPath);

            CementFile cement = new CementFile();
            cement.Deserialize(input);

            Console.WriteLine("{0} files in cement file.", cement.Entries.Count);

            foreach (CementEntry entry in cement.Entries)
            {
                CementMetadata metadata = cement.GetMetadata(entry.NameHash);

                string partPath;
                if (metadata == null)
                {
                    partPath = Path.Combine("__UNKNOWN", entry.NameHash.ToString("X8"));
                }
                else
                {
                    partPath = metadata.Name;
                }

                Console.WriteLine(partPath);

                Directory.CreateDirectory(Path.Combine(outputPath, Path.GetDirectoryName(partPath)));
                string entryPath = Path.Combine(outputPath, partPath);

                input.Seek(entry.Offset, SeekOrigin.Begin);

                Stream output = File.Open(entryPath, FileMode.Create, FileAccess.Write, FileShare.Read);

                long left = entry.Size;
                byte[] data = new byte[4096];
                while (left > 0)
                {
                    int block = (int)(Math.Min(left, 4096));
                    input.Read(data, 0, block);
                    output.Write(data, 0, block);
                    left -= block;
                }

                output.Close();
            }

            input.Close();
        }
    }
}
