using System;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00123003)]
    public class CompositeDrawableExpressionMixerReference : BaseNode
    {
        public UInt32 Unknown1 { get; set; }
        public string ExpressionMixerName { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteU32(this.Unknown1);
            output.WriteBASCII(this.ExpressionMixerName);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadU32();
            this.ExpressionMixerName = input.ReadBASCII();
        }
    }
}
