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
    [KnownCondition(typeof(Context.Scenario), "gameObjectAttribute")]
    public class ScenarioGameObjectAttribute : ConditionBase
    {
        public ScenarioGameObjectSlot GameObjectSlot;
        public string AttributeKey;
        public UInt64 ValueHash;

        public override void Serialize(Stream output, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input, FightFile fight)
        {
            this.GameObjectSlot = fight.ReadEnum<ScenarioGameObjectSlot>(input);
            this.AttributeKey = input.ReadAlignedASCII();
            this.ValueHash = fight.ReadHash100F4(input);
        }
    }
}
