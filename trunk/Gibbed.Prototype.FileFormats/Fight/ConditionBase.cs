using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight
{
    public abstract class ConditionBase
    {
        public UInt64 UnknownHash;

        public abstract void Serialize(Stream output, FightFile parent);
        public abstract void Deserialize(Stream input, FightFile parent);

        private static ConditionBase DeserializeCondition(UInt64 hash, Stream input, FightFile fight)
        {
            Type type = ConditionCache.GetType(fight.Context.GetType(), hash);
            if (type == null)
            {
                throw new InvalidOperationException("unknown condition type (" + FightHashes.Lookup(hash) + ")");
            }

            UInt32 length = input.ReadU32();

            ConditionBase condition;

            try
            {
                condition = (ConditionBase)System.Activator.CreateInstance(type);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }

            condition.Deserialize(input, fight);
            condition.UnknownHash = input.ReadU64();

            return condition;
        }

        public static List<ConditionBase> DeserializeConditions(string name, Stream input, FightFile fight)
        {
            if (input.ReadU64() != name.Hash1003F())
            {
                throw new Exception();
            }

            if (input.ReadU32() != 0)
            {
                throw new Exception();
            }

            List<ConditionBase> conditions = new List<ConditionBase>();

            while (true)
            {
                UInt64 hash = fight.ReadHash100F4(input);
                if (hash == 0)
                {
                    break;
                }

                conditions.Add(DeserializeCondition(hash, input, fight));
            }

            return conditions;
        }
    }
}
