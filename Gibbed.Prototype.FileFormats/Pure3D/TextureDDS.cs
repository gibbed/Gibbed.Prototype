using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;
using Gibbed.Squish;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00019006)]
    public class TextureDDS : BaseNode
    {
        public enum CompressionAlgorithm : uint
        {
            Unknown = 0,
            DXT1 = 0x31545844,
            DXT3 = 0x33545844,
            DXT5 = 0x35545844,
        }

        public string Name { get; set; }
        public UInt32 Version { get; set; }

        [Category("Image")]
        public UInt32 Width { get; set; }

        [Category("Image")]
        public UInt32 Height { get; set; }
        
        public UInt32 Unknown4 { get; set; }
        public UInt32 Unknown5 { get; set; }

        [Category("Image")]
        public UInt32 NumMipMaps { get; set; }

        [Category("Image")]
        public CompressionAlgorithm Algorithm { get; set; }

        public override string ToString()
        {
            if (this.Name == null || this.Name.Length == 0)
            {
                return base.ToString();
            }

            return base.ToString() + " (" + this.Name + ")";
        }

        public override void Serialize(Stream output)
        {
            output.WriteStringBASCII(this.Name);
            output.WriteValueU32(this.Version);
            output.WriteValueU32(this.Width);
            output.WriteValueU32(this.Height);
            output.WriteValueU32(this.Unknown4);
            output.WriteValueU32(this.Unknown5);
            output.WriteValueU32(this.NumMipMaps);
            output.WriteValueU32((UInt32)this.Algorithm);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringBASCII();
            this.Version = input.ReadValueU32();
            this.Width = input.ReadValueU32();
            this.Height = input.ReadValueU32();
            this.Unknown4 = input.ReadValueU32();
            this.Unknown5 = input.ReadValueU32();
            this.NumMipMaps = input.ReadValueU32();
            this.Algorithm = (CompressionAlgorithm)input.ReadValueU32();
        }

        private TextureData GetSubImageDataNode()
        {
            return (TextureData)this.Children.SingleOrDefault(candidate => candidate is TextureData);
        }

        public override object Preview()
        {
            TextureData data = this.GetSubImageDataNode();
            if (data == null)
            {
                return null;
            }

            MemoryStream memory = new MemoryStream();
            memory.Write(data.Data, 0, data.Data.Length);
            memory.Seek(0, SeekOrigin.Begin);

            try
            {
                DdsFile dds = new DdsFile();
                dds.Load(memory);
                return dds.Image();
            }
            catch (FormatException)
            {
                return null;
            }
        }

        public override bool Exportable
        {
            get
            {
                BaseNode node = this.GetSubImageDataNode();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Export(Stream output)
        {
            BaseNode node = this.GetSubImageDataNode();
            if (node == null)
            {
                throw new InvalidOperationException();
            }
            node.Export(output);
        }

        public override bool Importable
        {
            get
            {
                BaseNode node = this.GetSubImageDataNode();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Import(Stream input)
        {
            BaseNode node = this.GetSubImageDataNode();
            if (node == null)
            {
                throw new InvalidOperationException();
            }
            node.Import(input);
        }
    }
}
