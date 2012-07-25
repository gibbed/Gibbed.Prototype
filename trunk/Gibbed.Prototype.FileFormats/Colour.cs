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
using System.Runtime.Serialization;

namespace Gibbed.Prototype.FileFormats
{
    [DataContract(Namespace = "http://datacontract.gib.me/prototype")]
    public class Colour
    {
        [DataMember(Name = "r", Order = 1)]
        public float R { get; set; }

        [DataMember(Name = "g", Order = 2)]
        public float G { get; set; }

        [DataMember(Name = "b", Order = 3)]
        public float B { get; set; }

        [DataMember(Name = "a", Order = 4)]
        public float A { get; set; }

        public void Serialize(Stream output)
        {
            output.WriteValueF32(this.R);
            output.WriteValueF32(this.G);
            output.WriteValueF32(this.B);
            output.WriteValueF32(this.A);
        }

        public void Deserialize(Stream input)
        {
            this.R = input.ReadValueF32();
            this.G = input.ReadValueF32();
            this.B = input.ReadValueF32();
            this.A = input.ReadValueF32();
        }
    }
}
