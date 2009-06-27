using System;
using System.Collections.Generic;
using System.Reflection;

namespace Gibbed.Prototype.FileFormats.Fight
{
    internal abstract class Cache<T, A> where A : KnownHashAttribute
    {
        private static Dictionary<UInt64, Type> Lookup = null;

        private static void BuildLookup()
        {
            Lookup = new Dictionary<UInt64, Type>();

            foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes())
            {
                if (type.IsSubclassOf(typeof(T)) == true)
                {
                    object[] attributes = type.GetCustomAttributes(typeof(A), true);
                    foreach (object oattribute in attributes)
                    {
                        A attribute = (A)oattribute;
                        Lookup.Add(attribute.Hash, type);
                    }
                }
            }
        }

        public static Type GetType(UInt64 hash)
        {
            if (Lookup == null)
            {
                BuildLookup();
            }

            if (Lookup.ContainsKey(hash) == true)
            {
                return Lookup[hash];
            }

            return null;
        }
    }

    internal abstract class CacheForContext<T, A> where A : KnownHashForContextAttribute
    {
        private static Dictionary<Type, Dictionary<UInt64, Type>> Lookup = null;

        private static void BuildLookup()
        {
            Lookup = new Dictionary<Type, Dictionary<UInt64, Type>>();

            foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes())
            {
                if (type.IsSubclassOf(typeof(T)) == true)
                {
                    object[] attributes = type.GetCustomAttributes(typeof(A), true);
                    foreach (object oattribute in attributes)
                    {
                        A attribute = (A)oattribute;

                        if (Lookup.ContainsKey(attribute.Type) == false)
                        {
                            Lookup.Add(attribute.Type, new Dictionary<UInt64, Type>());
                        }

                        Lookup[attribute.Type].Add(attribute.Hash, type);
                    }
                }
            }
        }

        public static Type GetType(Type type, UInt64 hash)
        {
            if (Lookup == null)
            {
                BuildLookup();
            }

            if (Lookup.ContainsKey(type) == false)
            {
                return null;
            }

            if (Lookup[type].ContainsKey(hash) == true)
            {
                return Lookup[type][hash];
            }

            return null;
        }
    }
}
