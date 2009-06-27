using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Condition
{
    [KnownCondition(typeof(Context.Scenario), "stateMachine_CurrentState")]
    public class CurrentState : ConditionBase
    {
        public string State;
        public UInt32 Unknown;

        public override void Serialize(Stream output, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input, FightFile fight)
        {
            this.State = input.ReadAlignedASCII();
            if (this.State == null)
            {
                this.Unknown = input.ReadU32();
            }
        }
    }
}
