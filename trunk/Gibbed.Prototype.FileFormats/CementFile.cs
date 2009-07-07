using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats
{
    public class CementEntry
    {
        public UInt32 NameHash;
        public UInt32 Offset;
        public UInt32 Size;

        public void Serialize(Stream output, bool littleEndian)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input, bool littleEndian)
        {
            this.NameHash = input.ReadValueU32(littleEndian);
            this.Offset = input.ReadValueU32(littleEndian);
            this.Size = input.ReadValueU32(littleEndian);
        }
    }

    public class CementMetadata
    {
        public UInt32 TypeHash;
        public UInt32 Alignment;
        public UInt32 Unknown2;
        public string Name;
        public byte[] Unknown3;

        public void Serialize(Stream output, bool littleEndian)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input, bool littleEndian)
        {
            this.TypeHash = input.ReadValueU32();
            this.Alignment = input.ReadValueU32();
            this.Unknown2 = input.ReadValueU32();
            this.Name = input.ReadStringASCII(input.ReadValueU32(), true);
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
            Header header = input.ReadStructure<Header>();

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
                CementEntry entry = new CementEntry();
                entry.Deserialize(input, header.BigEndian == false);
                this.Entries.Add(entry);
            }

            input.Seek(header.MetadataOffset, SeekOrigin.Begin);
            UInt32 namesAlignment = input.ReadValueU32();
            UInt32 namesUnknown = input.ReadValueU32();
            this.Metadatas = new List<CementMetadata>();
            for (int i = 0; i < header.EntryCount; i++)
            {
                CementMetadata metadata = new CementMetadata();
                metadata.Deserialize(input, header.BigEndian == false);
                this.Metadatas.Add(metadata);
            }
        }
    }
}
