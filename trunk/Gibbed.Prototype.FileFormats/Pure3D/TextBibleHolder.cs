using System;
using System.Collections.Generic;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00018202)]
    public class TextBibleHolder : BaseNode
    {
        public string Language { get; set; }
        public UInt32 Version { get; set; }
        public List<String> Keys { get; set; }
        public List<UInt32> StringStarts { get; set; }
        public List<UInt32> StringStops { get; set; }

        public override void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input)
        {
            this.Language = input.ReadStringBASCII();
            this.Version = input.ReadValueU32();
            
            uint count = input.ReadValueU32();

            this.Keys = new List<string>();
            for (uint i = 0; i < count; i++)
            {
                this.Keys.Add(input.ReadStringBASCII());
            }

            this.StringStarts = new List<UInt32>();
            for (uint i = 0; i < count; i++)
            {
                this.StringStarts.Add(input.ReadValueU32());
            }

            this.StringStops = new List<UInt32>();
            for (uint i = 0; i < count; i++)
            {
                this.StringStops.Add(input.ReadValueU32());
            }
        }
    }
}
