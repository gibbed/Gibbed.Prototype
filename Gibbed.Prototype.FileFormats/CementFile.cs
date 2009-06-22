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

        public void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input)
        {
            this.NameHash = input.ReadU32();
            this.Offset = input.ReadU32();
            this.Size = input.ReadU32();
        }
    }

    public class CementMetadata
    {
        public UInt32 TypeHash;
        public UInt32 Alignment;
        public UInt32 Unknown2;
        public string Name;
        public byte[] Unknown3;

        public void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input)
        {
            this.TypeHash = input.ReadU32();
            this.Alignment = input.ReadU32();
            this.Unknown2 = input.ReadU32();
            this.Name = input.ReadASCII(input.ReadU32(), true);
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
            public byte VersionA;
            public byte VersionB;
            public byte VersionC;
            public byte VersionD;
            public UInt32 IndexOffset;
            public UInt32 IndexSize;
            public UInt32 MetadataOffset;
            public UInt32 MetadataSize;
            public UInt32 Unknown2;
            public UInt32 EntryCount;
        }

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
                throw new Exception("not a cement file");
            }

            if (header.VersionA != 2 || header.VersionB >= 2 || header.VersionC != 0 || header.VersionD == 0)
            {
                throw new Exception("bad version");
            }

            input.Seek(header.IndexOffset, SeekOrigin.Begin);
            this.Entries = new List<CementEntry>();
            for (int i = 0; i < header.EntryCount; i++)
            {
                CementEntry entry = new CementEntry();
                entry.Deserialize(input);
                this.Entries.Add(entry);
            }

            input.Seek(header.MetadataOffset, SeekOrigin.Begin);
            UInt32 namesAlignment = input.ReadU32();
            UInt32 namesUnknown = input.ReadU32();
            this.Metadatas = new List<CementMetadata>();
            for (int i = 0; i < header.EntryCount; i++)
            {
                CementMetadata metadata = new CementMetadata();
                metadata.Deserialize(input);
                this.Metadatas.Add(metadata);
            }
        }
    }
}
