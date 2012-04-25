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
using System.Reflection;

namespace Gibbed.Prototype.FileFormats.Fight
{
    internal abstract class Cache<TType, TAttribute> where TAttribute : KnownHashAttribute
    {
        private static Dictionary<ulong, Type> Lookup;

        private static void BuildLookup()
        {
            Lookup = new Dictionary<ulong, Type>();

            foreach (Type type in Assembly.GetAssembly(typeof(TType)).GetTypes())
            {
                if (type.IsSubclassOf(typeof(TType)) == true)
                {
                    object[] attributes = type.GetCustomAttributes(typeof(TAttribute), true);
                    foreach (object oattribute in attributes)
                    {
                        var attribute = (TAttribute)oattribute;
                        Lookup.Add(attribute.Hash, type);
                    }
                }
            }
        }

        public static Type GetType(ulong hash)
        {
            if (Lookup == null)
            {
                BuildLookup();
            }

            if (Lookup != null && Lookup.ContainsKey(hash) == true)
            {
                return Lookup[hash];
            }

            return null;
        }
    }

    internal abstract class CacheForContext<TType, TAttribute> where TAttribute : KnownHashForContextAttribute
    {
        private static Dictionary<Type, Dictionary<ulong, Type>> _Lookup;

        private static void BuildLookup()
        {
            _Lookup = new Dictionary<Type, Dictionary<ulong, Type>>();

            foreach (Type type in Assembly.GetAssembly(typeof(TType)).GetTypes())
            {
                if (type.IsSubclassOf(typeof(TType)) == true)
                {
                    object[] attributes = type.GetCustomAttributes(typeof(TAttribute), true);
                    foreach (object oattribute in attributes)
                    {
                        var attribute = (TAttribute)oattribute;

                        if (_Lookup.ContainsKey(attribute.Type) == false)
                        {
                            _Lookup.Add(attribute.Type, new Dictionary<UInt64, Type>());
                        }

                        _Lookup[attribute.Type].Add(attribute.Hash, type);
                    }
                }
            }
        }

        public static Type GetType(Type type, ulong hash)
        {
            if (_Lookup == null)
            {
                BuildLookup();
            }

            if (_Lookup != null && _Lookup.ContainsKey(type) == false)
            {
                return null;
            }

            if (_Lookup != null && _Lookup[type].ContainsKey(hash) == true)
            {
                return _Lookup[type][hash];
            }

            return null;
        }
    }
}
