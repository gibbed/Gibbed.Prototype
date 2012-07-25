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

using System;
using System.ComponentModel;
using System.IO;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    public class Unknown : BaseNode
    {
        private readonly uint _TypeId;

        public override uint TypeId
        {
            get { return this._TypeId; }
        }

        public Unknown(UInt32 typeId)
            : base()
        {
            this._TypeId = typeId;
        }

        /*
        [ReadOnly(true)]
        [DisplayName("Type ID")]
        public UInt32 TypeId { get; set; }
        */

        [ReadOnly(true)]
        [Category("Pure3D")]
        public byte[] Data { get; set; }

        [ReadOnly(true)]
        [Category("Pure3D")]
        public int Length
        {
            get { return this.Data == null ? -1 : this.Data.Length; }
        }

        public override string ToString()
        {
            return "Unknown (" + this.TypeId.ToString("X8") + ")";
        }

        public override void Serialize(Stream output)
        {
            throw new NotSupportedException();
        }

        public override void Deserialize(Stream input)
        {
            throw new NotSupportedException();
        }

        public override bool Exportable
        {
            get { return this.Data != null && this.Data.Length > 0; }
        }

        public override void Export(Stream output)
        {
            output.Write(this.Data, 0, this.Data.Length);
        }
    }
}
