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
    internal static class ContextCache
    {
        private static Dictionary<UInt64, Type> _Lookup;

        private static void BuildLookup()
        {
            _Lookup = new Dictionary<ulong, Type>();

            foreach (Type type in Assembly.GetAssembly(typeof(ContextCache)).GetTypes())
            {
                if (type.IsSubclassOf(typeof(ContextBase)) == true)
                {
                    object[] attributes = type.GetCustomAttributes(typeof(KnownContextAttribute), false);
                    if (attributes.Length == 1)
                    {
                        var attribute = (KnownContextAttribute)attributes[0];
                        _Lookup.Add(attribute.Hash, type);
                    }
                }
            }
        }

        public static Type GetContext(UInt64 hash)
        {
            if (_Lookup == null)
            {
                BuildLookup();
            }

            if (_Lookup != null && _Lookup.ContainsKey(hash))
            {
                return _Lookup[hash];
            }

            return null;
        }
    }
}
