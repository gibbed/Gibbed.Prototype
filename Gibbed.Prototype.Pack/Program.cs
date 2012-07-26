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
using Cement = Gibbed.Prototype.FileFormats.Cement;
using NDesk.Options;

namespace Gibbed.Prototype.Pack
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
                Console.WriteLine("Usage: {0} [OPTIONS]+ input_dir [output_rcf]", GetExecutableName());
                Console.WriteLine();
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            var inputPaths = new List<string>();
            inputPaths.Add(Path.GetFullPath(extras[0]));

            var outputPath = extras.Count > 1 ? extras[1] : Path.ChangeExtension(extras[0], ".rcf");

            var paths = new SortedDictionary<string, string>();

            if (verbose == true)
            {
                Console.WriteLine("Finding files...");
            }

            foreach (var relPath in inputPaths)
            {
                string inputPath = Path.GetFullPath(relPath);

                if (inputPath.EndsWith(Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture)) == true)
                {
                    inputPath = inputPath.Substring(0, inputPath.Length - 1);
                }

                foreach (string path in Directory.GetFiles(inputPath, "*", SearchOption.AllDirectories))
                {
                    string fullPath = Path.GetFullPath(path);
                    string partPath = fullPath.Substring(inputPath.Length + 1).ToLowerInvariant();

                    if (paths.ContainsKey(partPath) == true)
                    {
                        Console.WriteLine("Ignoring {0} duplicate.", partPath);
                        continue;
                    }

                    paths[partPath] = fullPath;
                }
            }

            using (var output = File.Create(outputPath))
            {
                var cement = new CementFile();
                cement.Endian = Endian.Little;

                cement.Entries.Clear();
                cement.Metadatas.Clear();

                foreach (var kv in paths)
                {
                    var entry = new Cement.Entry();
                    entry.NameHash = kv.Key.HashFileName(0);
                    entry.Offset = 0;
                    entry.Size = 0;

                    var metadata = new Cement.Metadata();
                    metadata.TypeHash = 0;
                    metadata.Alignment = 2048;
                    metadata.Name = kv.Key;

                    cement.Entries.Add(entry);
                    cement.Metadatas.Add(metadata);
                }

                var preestimatedHeaderSize = cement.EstimateHeaderSize();
                output.Seek(preestimatedHeaderSize, SeekOrigin.Begin);

                cement.Entries.Clear();
                cement.Metadatas.Clear();

                const uint fileAlignment = 2048;

                if (verbose == true)
                {
                    Console.WriteLine("Writing files to archive...");
                }

                foreach (var kvp in paths)
                {
                    if (verbose == true)
                    {
                        Console.WriteLine(kvp.Key);
                    }

                    using (var input = File.OpenRead(kvp.Value))
                    {
                        var entry = new Cement.Entry();
                        entry.NameHash = kvp.Key.HashFileName(0);
                        entry.Offset = (uint)output.Position;
                        entry.Size = (uint)input.Length;

                        /*var extension = Path.GetExtension(kvp.Key);
                        if (extension != null &&
                            extension.StartsWith(".") == true)
                        {
                            extension = extension.Substring(1);
                        }*/

                        var metadata = new Cement.Metadata();
                        metadata.TypeHash = 0;//extension == null ? 0 : extension.HashFileName(0);
                        metadata.Alignment = fileAlignment;
                        metadata.Name = kvp.Key;

                        cement.Entries.Add(entry);
                        cement.Metadatas.Add(metadata);

                        output.WriteFromStream(input, entry.Size);
                        output.Seek(output.Position.Align(fileAlignment), SeekOrigin.Begin);
                    }
                }

                var estimatedHeaderSize = cement.EstimateHeaderSize();
                if (preestimatedHeaderSize != estimatedHeaderSize)
                {
                    throw new InvalidOperationException();
                }

                if (verbose == true)
                {
                    Console.WriteLine("Writing header...");
                }

                output.Seek(0, SeekOrigin.Begin);
                cement.Serialize(output);
                
                if (output.Position != estimatedHeaderSize)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
