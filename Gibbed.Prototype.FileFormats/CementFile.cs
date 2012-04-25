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
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats
{
    public class CementEntry
    {
        public UInt32 NameHash;
        public UInt32 Offset;
        public UInt32 Size;

        public void Serialize(Stream output, Endian endian)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input, Endian endian)
        {
            this.NameHash = input.ReadValueU32(endian);
            this.Offset = input.ReadValueU32(endian);
            this.Size = input.ReadValueU32(endian);
        }
    }

    public class CementMetadata
    {
        public UInt32 TypeHash;
        public UInt32 Alignment;
        public UInt32 Unknown2;
        public string Name;
        public byte[] Unknown3;

        public void Serialize(Stream output, Endian endian)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input, Endian endian)
        {
            this.TypeHash = input.ReadValueU32();
            this.Alignment = input.ReadValueU32();
            this.Unknown2 = input.ReadValueU32();
            this.Name = input.ReadString(input.ReadValueU32(), true, Encoding.ASCII);
            this.Unknown3 = new byte[3];
            input.Read(this.Unknown3, 0, this.Unknown3.Length);
        }
    }

    public class CementFile
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 60, CharSet = CharSet.Ansi)]
        private struct Header
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
            public string Magic;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            private byte[] Padding1;

            public byte MajorVersion;
            public byte MinorVersion;

            [MarshalAs(UnmanagedType.I1)]
            public bool BigEndian;

            public byte Unknown1;
            public UInt32 IndexOffset;
            public UInt32 IndexSize;
            public UInt32 MetadataOffset;
            public UInt32 MetadataSize;
            public UInt32 Unknown2;
            public UInt32 EntryCount;
        }

        public byte MajorVersion;
        public byte MinorVersion;
        public bool BigEndian;
        public byte Unknown1;

        public List<CementEntry> Entries;
        public List<CementMetadata> Metadatas;

        public CementMetadata GetMetadata(UInt32 hash)
        {
            return this.Metadatas.SingleOrDefault(candidate => candidate.Name.PrototypeHash() == hash);
        }

        public void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input)
        {
            var header = input.ReadStructure<Header>();

            if (header.Magic != "ATG CORE CEMENT LIBRARY")
            {
                throw new FormatException("not a cement file");
            }

            if (header.MajorVersion != 2 || header.MinorVersion >= 2 || header.Unknown1 == 0)
            {
                throw new FormatException("bad cement version");
            }

            if (header.BigEndian == true)
            {
                header.IndexOffset = header.IndexOffset.Swap();
                header.IndexSize = header.IndexSize.Swap();
                header.MetadataOffset = header.MetadataOffset.Swap();
                header.MetadataSize = header.MetadataSize.Swap();
                header.Unknown2 = header.Unknown2.Swap();
                header.EntryCount = header.EntryCount.Swap();
            }

            this.MajorVersion = header.MajorVersion;
            this.MinorVersion = header.MinorVersion;
            this.BigEndian = header.BigEndian;
            this.Unknown1 = header.Unknown1;

            input.Seek(header.IndexOffset, SeekOrigin.Begin);
            this.Entries = new List<CementEntry>();
            for (int i = 0; i < header.EntryCount; i++)
            {
                var entry = new CementEntry();
                entry.Deserialize(input, header.BigEndian == false ? Endian.Little : Endian.Big);
                this.Entries.Add(entry);
            }

            input.Seek(header.MetadataOffset, SeekOrigin.Begin);
            UInt32 namesAlignment = input.ReadValueU32();
            UInt32 namesUnknown = input.ReadValueU32();
            this.Metadatas = new List<CementMetadata>();
            for (int i = 0; i < header.EntryCount; i++)
            {
                var metadata = new CementMetadata();
                metadata.Deserialize(input, header.BigEndian == false ? Endian.Little : Endian.Big);
                this.Metadatas.Add(metadata);
            }
        }
    }
}
