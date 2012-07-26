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
    [KnownType(0x00002200)]
    public class Camera : BaseNode
    {
        public string Name { get; set; }
        public uint Version { get; set; }
        public float FOV { get; set; }
        public float AspectRatio { get; set; }
        public float NearClip { get; set; }
        public float FarClip { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Look { get; set; }
        public Vector3 Up { get; set; }

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
            output.WriteValueU32(this.Version);
            output.WriteValueF32(this.FOV);
            output.WriteValueF32(this.AspectRatio);
            output.WriteValueF32(this.NearClip);
            output.WriteValueF32(this.FarClip);
            this.Position.Serialize(output);
            this.Look.Serialize(output);
            this.Up.Serialize(output);
        }

        public override void Deserialize(Stream input)
        {
            this.Name = input.ReadStringAlignedU8();
            this.Version = input.ReadValueU32();
            this.FOV = input.ReadValueF32();
            this.AspectRatio = input.ReadValueF32();
            this.NearClip = input.ReadValueF32();
            this.FarClip = input.ReadValueF32();
            this.Position = new Vector3(input);
            this.Look = new Vector3(input);
            this.Up = new Vector3(input);
        }
    }
}
