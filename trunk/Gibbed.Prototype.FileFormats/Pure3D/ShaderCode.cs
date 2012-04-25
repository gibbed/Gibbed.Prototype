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
using System.Text;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x0001100C)]
    public class ShaderCode : BaseNode
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public interface ICode
        {
            void Serialize(Stream output);
            void Deserialize(Stream input);
        }

        public class CompiledCode : ICode
        {
            public byte[] Data { get; set; }

            public void Serialize(Stream output)
            {
                output.Write(this.Data, 0, this.Data.Length);
            }

            public void Deserialize(Stream input)
            {
                // TODO: figure out format? :)

                this.Data = new byte[input.Length];
                input.Read(this.Data, 0, this.Data.Length);
            }
        }

        public class SourceCode : ICode
        {
            public string Text { get; set; }

            public void Serialize(Stream output)
            {
                output.WriteStringZ(this.Text, Encoding.ASCII);
            }

            public void Deserialize(Stream input)
            {
                this.Text = input.ReadString((uint)input.Length, true, Encoding.ASCII);
            }
        }

        [DisplayName("Global Variable Count")]
        public uint GlobalVariableCount { get; set; }

        public ICode Code { get; set; }

        public override string ToString()
        {
            if (this.Code == null)
            {
                return base.ToString();
            }

            return base.ToString() + " (" + this.Code.GetType().Name + ")";
        }

        public override void Serialize(Stream output)
        {
            if (this.Code is SourceCode)
            {
                output.WriteValueU32(1);
            }
            else if (this.Code is CompiledCode)
            {
                output.WriteValueU32(5);
            }
            else
            {
                throw new InvalidOperationException();
            }

            Stream codeStream = new MemoryStream();
            this.Code.Serialize(codeStream);
            codeStream.Seek(0, SeekOrigin.Begin);

            output.WriteValueS32((int)codeStream.Length);
            output.WriteFromStream(codeStream, codeStream.Length);
        }

        public override void Deserialize(Stream input)
        {
            var dataType = input.ReadValueU32();
            var length = input.ReadValueS32();
            this.GlobalVariableCount = input.ReadValueU32();

            var codeStream = input.ReadToMemoryStream(length);
            ICode code;

            if (dataType == 1)
            {
                code = new SourceCode();
            }
            else if (dataType == 5)
            {
                code = new CompiledCode();
            }
            else
            {
                throw new InvalidOperationException();
            }

            code.Deserialize(codeStream);
            this.Code = code;
        }

        public override bool Exportable
        {
            get { return this.Code != null; }
        }

        public override void Export(Stream output)
        {
            // a hack but it'll work for now
            this.Code.Serialize(output);
        }

        public override bool Importable
        {
            get { return this.Code != null; }
        }

        public override void Import(Stream input)
        {
            // a hack but it'll work for now
            this.Code.Deserialize(input);
        }
    }
}
