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

        public List<Cement.Entry> Entries
            = new List<Cement.Entry>();
        public List<Cement.Metadata> Metadatas
            = new List<Cement.Metadata>();

        public Cement.Metadata GetMetadata(UInt32 hash)
        {
            return this.Metadatas.SingleOrDefault(candidate => candidate.Name.HashFileName() == hash);
        }

        public void Serialize(Stream output)
        {
            throw new NotImplementedException();
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
