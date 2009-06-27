using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Condition
{
    [KnownCondition(typeof(Context.Scenario), "event_String")]
    public class ScenarioString : ConditionBase
    {
        public UInt64 StringHash;

        public override void Serialize(Stream output, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input, FightFile fight)
        {
            this.StringHash = input.ReadU64();
        }
    }
}
