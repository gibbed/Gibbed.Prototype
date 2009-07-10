using System.IO;
using Gibbed.Prototype.Helpers;
using Gibbed.Helpers;
using System;
using System.Globalization;
using System.Drawing;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00010020)]
    public class U00010020_PrimitiveGroup : BaseNode
    {
        public UInt32 Version { get; set; }
        public string ShaderName { get; set; }
        public UInt32 PrimitiveType { get; set; }
        public UInt32 VertexType { get; set; }
        public UInt32 NumVertices { get; set; }
        public UInt32 NumIndices { get; set; }
        public UInt32 NumMatrices { get; set; } // skeleton bones / blendindices
        public UInt32 MemoryImaged { get; set; }
        public UInt32 Optimized { get; set; }
        public UInt32 VertexAnimated { get; set; }
        public UInt32 VertexAnimationMask { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteValueU32(this.Version);
            output.WriteStringBASCII(this.ShaderName);
            output.WriteValueU32(this.PrimitiveType);
            output.WriteValueU32(this.VertexType);
            output.WriteValueU32(this.NumVertices);
            output.WriteValueU32(this.NumIndices);
            output.WriteValueU32(this.NumMatrices);
            output.WriteValueU32(this.MemoryImaged);
            output.WriteValueU32(this.Optimized);
            output.WriteValueU32(this.VertexAnimated);
            output.WriteValueU32(this.VertexAnimationMask);
        }

        public override void Deserialize(Stream input)
        {
            this.Version = input.ReadValueU32();
            this.ShaderName = input.ReadStringBASCII();
            this.PrimitiveType = input.ReadValueU32();
            this.VertexType = input.ReadValueU32();
            this.NumVertices = input.ReadValueU32();
            this.NumIndices = input.ReadValueU32();
            this.NumMatrices = input.ReadValueU32();
            this.MemoryImaged = input.ReadValueU32();
            this.Optimized = input.ReadValueU32();
            this.VertexAnimated = input.ReadValueU32();
            this.VertexAnimationMask = input.ReadValueU32();
        }
    }
}
