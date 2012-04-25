﻿/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
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

using System.ComponentModel;
using System.IO;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x00121006)]
    public class U00121006 : BaseNode
    {
        public uint Unknown1 { get; set; }

        [DisplayName("Count of 00121001's")]
        public uint Unknown2 { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteValueU32(this.Unknown1);
            output.WriteValueU32(this.Unknown2);
        }

        public override void Deserialize(Stream input)
        {
            this.Unknown1 = input.ReadValueU32();
            this.Unknown2 = input.ReadValueU32();
        }
    }
}