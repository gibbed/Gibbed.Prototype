using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00019001)]
    public class ImagePNG : Node
    {
        public string Name { get; set; }
        public UInt32 Unknown1 { get; set; }
        public UInt32 Unknown2 { get; set; }
        public UInt32 Unknown3 { get; set; }
        public UInt32 Unknown4 { get; set; }
        public UInt32 Unknown5 { get; set; }
        public UInt32 Unknown6 { get; set; }
        public UInt32 Unknown7 { get; set; }

        public override string ToString()
        {
            if (this.Name == null || this.Name.Length == 0)
            {
                return "Image PNG";
            }

            return "Image PNG (" + this.Name + ")";
        }

        public override void Serialize(Stream output)
        {
            output.WriteBASCII(this.Name);
            output.WriteU32(this.Unknown1);
            output.WriteU32(this.Unknown2);
            output.WriteU32(this.Unknown3);
            output.WriteU32(this.Unknown4);
            output.WriteU32(this.Unknown5);
            output.WriteU32(this.Unknown6);
            output.WriteU32(this.Unknown7);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadBASCII();
            this.Unknown1 = input.ReadU32();
            this.Unknown2 = input.ReadU32();
            this.Unknown3 = input.ReadU32();
            this.Unknown4 = input.ReadU32();
            this.Unknown5 = input.ReadU32();
            this.Unknown6 = input.ReadU32();
            this.Unknown7 = input.ReadU32();
        }

        public override System.Drawing.Image Preview()
        {
            ImageData data = (ImageData)this.Children.SingleOrDefault(candidate => candidate is ImageData);
            if (data == null)
            {
                return null;
            }

            MemoryStream memory = new MemoryStream();
            memory.Write(data.Data, 0, data.Data.Length);
            memory.Seek(0, SeekOrigin.Begin);

            return Bitmap.FromStream(memory);
        }
    }
}
