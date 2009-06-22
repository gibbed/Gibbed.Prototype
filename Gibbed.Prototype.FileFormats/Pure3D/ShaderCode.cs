using System;
using System.ComponentModel;
using System.IO;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    [KnownType(0x0001100C)]
    public class ShaderCode : Node
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
                output.WriteASCII(this.Text);
                output.WriteU8(0);
            }

            public void Deserialize(Stream input)
            {
                this.Text = input.ReadASCII((UInt32)(input.Length), true);
            }
        }

        [DisplayName("Global Variable Count")]
        public UInt32 GlobalVariableCount { get; set; }
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
                output.WriteU32(1);
            }
            else if (this.Code is CompiledCode)
            {
                output.WriteU32(5);
            }
            else
            {
                throw new InvalidOperationException();
            }

            Stream codeStream = new MemoryStream();
            this.Code.Serialize(codeStream);
            codeStream.Seek(0, SeekOrigin.Begin);

            output.WriteS32((int)codeStream.Length);
            output.WriteFromStream(codeStream, codeStream.Length);
        }

        public override void Deserialize(Stream input)
        {
            UInt32 dataType = input.ReadU32();
            int length = input.ReadS32();
            this.GlobalVariableCount = input.ReadU32();

            Stream codeStream = input.ReadToMemoryStream(length);
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
            get
            {
                return this.Code != null;
            }
        }

        public override void Export(Stream output)
        {
            // a hack but it'll work for now
            this.Code.Serialize(output);
        }

        public override bool Importable
        {
            get
            {
                return this.Code != null;
            }
        }

        public override void Import(Stream input)
        {
            // a hack but it'll work for now
            this.Code.Deserialize(input);
        }
    }
}
