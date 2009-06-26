using System;
using System.Collections.Generic;
using System.Reflection;

namespace Gibbed.Prototype.FileFormats.Fight
{
    public static class ConditionCache
    {
        private static Dictionary<Type, Dictionary<UInt64, Type>> Lookup = null;

        private static void BuildLookup()
        {
            Lookup = new Dictionary<Type, Dictionary<UInt64, Type>>();

            foreach (Type type in Assembly.GetAssembly(typeof(ConditionCache)).GetTypes())
            {
                if (type.IsSubclassOf(typeof(Fight.ConditionBase)) == true)
                {
                    object[] attributes = type.GetCustomAttributes(typeof(Fight.KnownConditionAttribute), false);
                    foreach (object oattribute in attributes)
                    {
                        Fight.KnownConditionAttribute attribute = (Fight.KnownConditionAttribute)oattribute;

                        if (Lookup.ContainsKey(attribute.BranchType) == false)
                        {
                            Lookup.Add(attribute.BranchType, new Dictionary<UInt64, Type>());
                        }

                        Lookup[attribute.BranchType].Add(attribute.Hash, type);
                    }
                }
            }
        }

        public static Type GetCondition(Type branchType, UInt64 hash)
        {
            if (Lookup == null)
            {
                BuildLookup();
            }

            if (Lookup.ContainsKey(branchType) == false)
            {
                return null;
            }

            if (Lookup[branchType].ContainsKey(hash) == false)
            {
                return null;
            }

            return Lookup[branchType][hash];
        }
    }
}
