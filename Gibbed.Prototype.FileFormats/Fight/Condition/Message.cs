using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Condition
{
    public enum MessageType : ulong
    {
        None = 0x004E39CF41EA1FD6,
        Collision = 0x22722E853938D49C,
        ApplyHit = 0x6024E084A23A0FBF,
        ApplyDamage = 0x55B1CC9B1858C877,
        DamageApplied = 0x4B56A337189040E2,
        Throw = 0x52E69CC8AF3ADF94,
        PlaybackFinished = 0xE9F727D5B2F75675,
        SetPropLogicState = 0xE27C5F6E2CBC200A,
    }

    [KnownCondition(typeof(Context.PropLogic), "message")]
    public class Message : ConditionBase
    {
        public MessageType Type;

        public override void Serialize(Stream output, FightFile fight)
        {
            output.WriteValueU64((UInt64)(this.Type));
        }

        public override void Deserialize(Stream input, FightFile fight)
        {
            this.Type = fight.ReadPropertyEnum<MessageType>(input);
        }
    }
}
