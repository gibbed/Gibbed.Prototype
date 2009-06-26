using System;
using System.Collections.Generic;
using System.Reflection;

namespace Gibbed.Prototype.FileFormats.Fight
{
    internal static class ContextCache
    {
        private static Dictionary<UInt64, Type> Lookup = null;

        private static void BuildLookup()
        {
            Lookup = new Dictionary<UInt64, Type>();

            foreach (Type type in Assembly.GetAssembly(typeof(ContextCache)).GetTypes())
            {
                if (type.IsSubclassOf(typeof(Fight.ContextBase)) == true)
                {
                    object[] attributes = type.GetCustomAttributes(typeof(Fight.KnownContextAttribute), false);
                    if (attributes.Length == 1)
                    {
                        Fight.KnownContextAttribute attribute = (Fight.KnownContextAttribute)attributes[0];
                        Lookup.Add(attribute.Hash, type);
                    }
                }
            }
        }

        public static Type GetContext(UInt64 hash)
        {
            if (Lookup == null)
            {
                BuildLookup();
            }

            if (Lookup.ContainsKey(hash))
            {
                return Lookup[hash];
            }

            return null;
        }
    }
}
