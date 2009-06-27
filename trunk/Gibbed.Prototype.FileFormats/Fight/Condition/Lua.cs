using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Condition
{
    [KnownCondition(typeof(Context.Scenario), "lua")]
    public class Lua : ConditionBase
    {
        public byte[] Script;
        public string ScriptText
        {
            get
            {
                if (this.Script.Length < 5)
                {
                    return Encoding.ASCII.GetString(this.Script);
                }

                if (this.Script[0] == 0x1B &&
                    this.Script[1] == 0x4C &&
                    this.Script[2] == 0x75 &&
                    this.Script[3] == 0x61 &&
                    this.Script[4] == 0x51)
                {
                    return null;
                }

                return Encoding.ASCII.GetString(this.Script);
            }
        }

        public override void Serialize(Stream output, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input, FightFile fight)
        {
            int length = input.ReadS32();
            this.Script = new byte[length];
            input.ReadAligned(this.Script, 0, length, 4);
        }
    }
}
