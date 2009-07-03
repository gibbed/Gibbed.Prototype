using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;
using System.Xml.Serialization;

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
            this.GameObjectSlot = fight.ReadPropertyEnum<ScenarioGameObjectSlot>(input);
            this.AttributeKey = fight.ReadPropertyString(input);
            this.ValueHash = fight.ReadPropertyName(input);
        }
    }
}
