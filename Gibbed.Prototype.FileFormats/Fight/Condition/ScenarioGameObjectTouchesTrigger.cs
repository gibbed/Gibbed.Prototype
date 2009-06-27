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
    public enum Direction : ulong
    {
        Enter = 0x439ECB8EDE3C7E0E,
        Exit = 0x004532854F5604D8,
    }

    [KnownCondition(typeof(Context.Scenario), "gameObjectTriggerTouch")]
    public class ScenarioGameObjectTouchesTrigger : ConditionBase
    {
        public UInt64 TriggerNameHash;
        public UInt64 GameObjectNameHash;
        public Direction Direction;

        public override void Serialize(Stream output, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input, FightFile fight)
        {
            this.GameObjectNameHash = fight.ReadNameHash(input);
            this.TriggerNameHash = fight.ReadNameHash(input);
            this.Direction = fight.ReadEnum<Direction>(input);
        }
    }
}
