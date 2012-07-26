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

using System.IO;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00011009)]
    public class ShaderTemplate : BaseNode
    {
        public string Name { get; set; }
        public uint Unknown02 { get; set; }
        public uint NumPasses { get; set; }
        public uint Unknown04 { get; set; }
        public uint Unknown05 { get; set; }
        public uint NumFloat2 { get; set; }
        public uint Unknown07 { get; set; }
        public uint Unknown08 { get; set; }
        public uint Unknown09 { get; set; }
        public uint NumFloat3 { get; set; }
        public uint Unknown11 { get; set; }
        public uint Unknown12 { get; set; }
        public uint NumFloat4 { get; set; }
        public uint NumMatrices { get; set; }
        public uint NumBools { get; set; }
        public uint Unknown16 { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Name) == true)
            {
                return base.ToString();
            }

            return base.ToString() + " (" + this.Name + ")";
        }

        public override void Serialize(Stream output)
        {
            output.WriteStringAlignedU8(this.Name);
            output.WriteValueU32(this.Unknown02);
            output.WriteValueU32(this.NumPasses);
            output.WriteValueU32(this.Unknown04);
            output.WriteValueU32(this.Unknown05);
            output.WriteValueU32(this.NumFloat2);
            output.WriteValueU32(this.Unknown07);
            output.WriteValueU32(this.Unknown08);
            output.WriteValueU32(this.Unknown09);
            output.WriteValueU32(this.NumFloat3);
            output.WriteValueU32(this.Unknown11);
            output.WriteValueU32(this.Unknown12);
            output.WriteValueU32(this.NumFloat4);
            output.WriteValueU32(this.NumMatrices);
            output.WriteValueU32(this.NumBools);
            output.WriteValueU32(this.Unknown16);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringAlignedU8();
            this.Unknown02 = input.ReadValueU32();
            this.NumPasses = input.ReadValueU32();
            this.Unknown04 = input.ReadValueU32();
            this.Unknown05 = input.ReadValueU32();
            this.NumFloat2 = input.ReadValueU32();
            this.Unknown07 = input.ReadValueU32();
            this.Unknown08 = input.ReadValueU32();
            this.Unknown09 = input.ReadValueU32();
            this.NumFloat3 = input.ReadValueU32();
            this.Unknown11 = input.ReadValueU32();
            this.Unknown12 = input.ReadValueU32();
            this.NumFloat4 = input.ReadValueU32();
            this.NumMatrices = input.ReadValueU32();
            this.NumBools = input.ReadValueU32();
            this.Unknown16 = input.ReadValueU32();
        }
    }
}
