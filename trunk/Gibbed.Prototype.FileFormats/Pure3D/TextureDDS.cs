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
    public class TextureDDS : Node
    {
        public enum CompressionAlgorithm : uint
        {
            Unknown = 0,
            DXT1 = 0x31545844,
            DXT3 = 0x33545844,
            DXT5 = 0x35545844,
        }

        public string Name { get; set; }
        public UInt32 Unknown1 { get; set; }

        [Category("Image")]
        public UInt32 Width { get; set; }

        [Category("Image")]
        public UInt32 Height { get; set; }
        
        public UInt32 Unknown4 { get; set; }
        public UInt32 Unknown5 { get; set; }

        [Category("Image")]
        public UInt32 Mips { get; set; }

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
            output.WriteBASCII(this.Name);
            output.WriteU32(this.Unknown1);
            output.WriteU32(this.Width);
            output.WriteU32(this.Height);
            output.WriteU32(this.Unknown4);
            output.WriteU32(this.Unknown5);
            output.WriteU32(this.Mips);
            output.WriteU32((UInt32)this.Algorithm);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadBASCII();
            this.Unknown1 = input.ReadU32();
            this.Width = input.ReadU32();
            this.Height = input.ReadU32();
            this.Unknown4 = input.ReadU32();
            this.Unknown5 = input.ReadU32();
            this.Mips = input.ReadU32();
            this.Algorithm = (CompressionAlgorithm)input.ReadU32();
        }

        private TextureData GetSubImageDataNode()
        {
            return (TextureData)this.Children.SingleOrDefault(candidate => candidate is TextureData);
        }

        public override System.Drawing.Image Preview()
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
                Node node = this.GetSubImageDataNode();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Export(Stream output)
        {
            Node node = this.GetSubImageDataNode();
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
                Node node = this.GetSubImageDataNode();
                if (node == null)
                {
                    return false;
                }
                return node.Exportable;
            }
        }

        public override void Import(Stream input)
        {
            Node node = this.GetSubImageDataNode();
            if (node == null)
            {
                throw new InvalidOperationException();
            }
            node.Import(input);
        }
    }
}
