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
    [KnownType(0x07F00001)]
    public class MetaObjectData : BaseNode
    {
        public byte[] Data { get; set; }

        public override void Serialize(Stream output)
        {
            output.WriteValueS32(this.Data.Length);
            output.Write(this.Data, 0, this.Data.Length);
        }

        public override void Deserialize(Stream input)
        {
            var length = input.ReadValueS32();
            this.Data = new byte[length];
            input.Read(this.Data, 0, this.Data.Length);
        }

        public override bool Exportable
        {
            get { return this.Data != null && this.Data.Length > 0; }
        }

        public override void Export(Stream output)
        {
            output.Write(this.Data, 0, this.Data.Length);
        }

        public override bool Importable
        {
            get { return true; }
        }

        public override void Import(Stream input)
        {
            this.Data = new byte[input.Length];
            input.Read(this.Data, 0, this.Data.Length);
        }
    }
}
