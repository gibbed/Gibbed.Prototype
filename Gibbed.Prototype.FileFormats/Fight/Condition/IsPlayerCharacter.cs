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
    [KnownCondition(typeof(Context.Scenario), "isPlayerCharacter")]
    public class IsPlayerCharacter : ConditionBase
    {
        public ScenarioGameObjectSlot Affiliate;

        public override void Serialize(Stream output, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input, FightFile fight)
        {
            this.Affiliate = fight.ReadEnum<ScenarioGameObjectSlot>(input);
        }
    }
}
