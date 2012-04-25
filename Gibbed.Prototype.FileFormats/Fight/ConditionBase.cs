/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Gibbed.IO;

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

            var length = input.ReadValueU32();

            ConditionBase condition;

            try
            {
                condition = (ConditionBase)Activator.CreateInstance(type);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }

            condition.Deserialize(input, fight);
            condition.UnknownHash = input.ReadValueU64();

            return condition;
        }

        public static List<ConditionBase> DeserializeConditions(string name, Stream input, FightFile fight)
        {
            if (input.ReadValueU64() != name.Hash1003F())
            {
                throw new Exception();
            }

            if (input.ReadValueU32() != 0)
            {
                throw new Exception();
            }

            var conditions = new List<ConditionBase>();

            while (true)
            {
                var hash = fight.ReadHash(input);
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
