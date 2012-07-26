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
    [KnownType(0x00121000)]
    public class Animation : BaseNode
    {
        public uint Version { get; set; }
        public string Name { get; set; }
        public FourCC AnimationType { get; set; }
        public float NumFrames { get; set; }
        public float FrameRate { get; set; }
        public uint Cyclic { get; set; }

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
            output.WriteValueU32(this.Version);
            output.WriteStringAlignedU8(this.Name);
            this.AnimationType.Serialize(output);
            output.WriteValueF32(this.NumFrames);
            output.WriteValueF32(this.FrameRate);
            output.WriteValueU32(this.Cyclic);
        }

        public override void Deserialize(Stream input)
        {
            this.Version = input.ReadValueU32();
            this.Name = input.ReadStringAlignedU8();
            this.AnimationType = new FourCC(input);
            this.NumFrames = input.ReadValueF32();
            this.FrameRate = input.ReadValueF32();
            this.Cyclic = input.ReadValueU32();
        }
    }
}
