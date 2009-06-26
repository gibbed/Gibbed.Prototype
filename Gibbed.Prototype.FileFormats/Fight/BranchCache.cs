using System;
using System.Collections.Generic;
using System.Reflection;

namespace Gibbed.Prototype.FileFormats.Fight
{
    internal static class BranchCache
    {
        private static Dictionary<Type, Dictionary<UInt64, Type>> Lookup = null;

        private static void BuildLookup()
        {
            Lookup = new Dictionary<Type, Dictionary<UInt64, Type>>();

            foreach (Type type in Assembly.GetAssembly(typeof(BranchCache)).GetTypes())
            {
                if (type.IsSubclassOf(typeof(Fight.BranchBase)) == true)
                {
                    object[] attributes = type.GetCustomAttributes(typeof(Fight.KnownBranchAttribute), false);
                    foreach (object oattribute in attributes)
                    {
                        Fight.KnownBranchAttribute attribute = (Fight.KnownBranchAttribute)oattribute;

                        if (Lookup.ContainsKey(attribute.ContextType) == false)
                        {
                            Lookup.Add(attribute.ContextType, new Dictionary<UInt64, Type>());
                        }

                        Lookup[attribute.ContextType].Add(attribute.Hash, type);
                    }
                }
            }
        }

        public static Type GetBranch(Type contextType, UInt64 hash)
        {
            if (Lookup == null)
            {
                BuildLookup();
            }

            if (Lookup.ContainsKey(contextType) == false)
            {
                return null;
            }

            if (Lookup[contextType].ContainsKey(hash) == false)
            {
                return null;
            }

            return Lookup[contextType][hash];
        }
    }
}
