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
using System.Text;
using System.IO;
using System.Linq;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats
{
    public class CementFile
    {
        public Endian Endian;

        public byte MajorVersion;
        public byte MinorVersion;

        public readonly List<Cement.Entry> Entries = new List<Cement.Entry>();
        public readonly List<Cement.Metadata> Metadatas = new List<Cement.Metadata>();

        public Cement.Metadata GetMetadata(UInt32 hash)
        {
            return this.Metadatas.SingleOrDefault(candidate => candidate.Name.HashFileName() == hash);
        }

        public int EstimateHeaderSize()
        {
            int size = 0;

            size += 24; // magic
            size += 8; // padding
            size += 1 + 1 + 1 + 1 + 4 + 4 + 4 + 4 + 4 + 4; // header
            
            size += this.EstimateEntryTableSize();
            size = size.Align(2048);

            size += this.EstimateMetadataTableSize();
            size = size.Align(2048);

            return size;
        }

        public int EstimateEntryTableSize()
        {
            return this.Entries.Sum(e => e.ByteSize);
        }

        public int EstimateMetadataTableSize()
        {
            return 4 + 4 + this.Metadatas.Sum(e => e.ByteSize);
        }

        public void Serialize(Stream output)
        {
            var endian = this.Endian;

            output.WriteString("ATG CORE CEMENT LIBRARY", 24, Encoding.ASCII);
            output.Seek(8, SeekOrigin.Current);
            
            output.WriteValueU8(2); // majorVersion
            output.WriteValueU8(1); // minorVersion

            switch (endian)
            {
                case Endian.Little:
                {
                    output.WriteValueB8(false);
                    break;
                }

                case Endian.Big:
                {
                    output.WriteValueB8(true);
                    break;
                }

                default:
                {
                    throw new InvalidOperationException("unsupported endian");
                }
            }

            output.WriteValueU8(1); // unknown1

            var offset = 0x3C;
            
            var indexSize = this.EstimateEntryTableSize();
            output.WriteValueS32(offset, endian); // indexOffset
            output.WriteValueS32(indexSize, endian); // indexSize
            offset += indexSize;
            offset = offset.Align(2048);

            var metadataSize = this.EstimateMetadataTableSize();
            output.WriteValueS32(offset, endian); // metadataOffset
            output.WriteValueS32(metadataSize, endian); // metadataSize
            //offset += metadataSize;

            output.WriteValueU32(0, endian); // unknown2
            output.WriteValueS32(this.Entries.Count, endian); // entryCount

            foreach (var entry in this.Entries)
            {
                entry.Serialize(output, endian);
            }

            output.Seek(output.Position.Align(2048), SeekOrigin.Begin);

            output.WriteValueU32(2048, Endian.Little);
            output.WriteValueU32(0, Endian.Little);
            foreach (var metadata in this.Metadatas)
            {
                metadata.Serialize(output, endian);
            }

            output.Seek(output.Position.Align(2048), SeekOrigin.Begin);
        }

        public void Deserialize(Stream input)
        {
            var magic = input.ReadString(24, true, Encoding.ASCII);
            if (magic != "ATG CORE CEMENT LIBRARY")
            {
                throw new FormatException("not a cement file");
            }

            input.ReadBytes(8); // padding

            var majorVersion = input.ReadValueU8();
            var minorVersion = input.ReadValueU8();
            var endian = input.ReadValueB8() == false ? Endian.Little : Endian.Big;
            var unknown1 = input.ReadValueU8();

            if (majorVersion != 2 ||
                minorVersion != 1 ||
                unknown1 != 1)
            {
                throw new FormatException("bad cement version");
            }

            var indexOffset = input.ReadValueU32(endian);
            var indexSize = input.ReadValueU32(endian);
            var metadataOffset = input.ReadValueU32(endian);
            var metadataSize = input.ReadValueU32(endian);
            var unknown2 = input.ReadValueU32(endian);
            var entryCount = input.ReadValueU32(endian);

            if (unknown2 != 0)
            {
                throw new FormatException();
            }

            this.MajorVersion = majorVersion;
            this.MinorVersion = minorVersion;

            input.Seek(indexOffset, SeekOrigin.Begin);
            using (var temp = input.ReadToMemoryStream(indexSize))
            {
                this.Entries.Clear();
                for (int i = 0; i < entryCount; i++)
                {
                    var entry = new Cement.Entry();
                    entry.Deserialize(temp, endian);
                    this.Entries.Add(entry);
                }

                if (temp.Position != temp.Length)
                {
                    throw new FormatException();
                }
            }

            input.Seek(metadataOffset, SeekOrigin.Begin);
            using (var temp = input.ReadToMemoryStream(metadataSize))
            {
                var namesAlignment = temp.ReadValueU32(Endian.Little);
                var namesUnknown = temp.ReadValueU32(Endian.Little);

                this.Metadatas.Clear();
                for (int i = 0; i < entryCount; i++)
                {
                    var metadata = new Cement.Metadata();
                    metadata.Deserialize(temp, endian);
                    this.Metadatas.Add(metadata);
                }
            }

            this.Endian = endian;
        }
    }
}
