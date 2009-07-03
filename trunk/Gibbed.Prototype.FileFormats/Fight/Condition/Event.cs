using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Condition
{
    public enum EventWhenType : ulong
    {
        OnEnter = 0x40DDC6BC9ABB122F,
        OnExit = 0x934B015A6711A83F,
    }

    //[KnownCondition("event")]
    public class Event : ConditionBase
    {
        public UInt64 EventHash;
        public EventWhenType When;

        public override void Serialize(Stream output, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input, FightFile fight)
        {
            this.EventHash = input.ReadU64();
            this.When = fight.ReadPropertyEnum<EventWhenType>(input);
        }
    }
}
