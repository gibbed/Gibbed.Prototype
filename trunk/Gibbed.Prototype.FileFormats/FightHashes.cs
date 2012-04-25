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

namespace Gibbed.Prototype.FileFormats
{
    public enum FightHash : ulong
    {
        Chunk = 0x616FE21D386330FF, // "chunk"
        Bank = 0x006248CCFF4DC4FA, // "bank"
        Node = 0x006E51B53282267C, // "node"
        Store = 0x7149C22710BC52F3, // "store"
        Reference = 0x87E400FE4359E3E7, // "reference"
    }

    public static class FightHashes
    {
        private static Dictionary<UInt64, string> _RegisteredLookup;
        private static List<Type> _RegisteredEnum;

        public static void Register(string text)
        {
            if (_RegisteredLookup == null)
            {
                _RegisteredLookup = new Dictionary<UInt64, string>();
            }

            _RegisteredLookup[text.Hash1003F()] = text;
        }

        public static string Lookup(UInt64 hash)
        {
            if (_RegisteredLookup != null && _RegisteredLookup.ContainsKey(hash) == true)
            {
                return "\"" + _RegisteredLookup[hash] + "\" == " + hash.ToString("X16");
            }

            if (_RegisteredEnum == null)
            {
                _RegisteredEnum = new List<Type>
                {
                    typeof(Fight.Condition.ScenarioGameObjectSlot),
                    typeof(Fight.Condition.ScenarioMessageType),
                    typeof(Fight.Condition.EventWhenType),
                    typeof(Fight.Track.MusicPriority),
                    typeof(FightHash)
                };
            }

            if (_RegisteredEnum != null)
            {
                foreach (Type e in _RegisteredEnum)
                {
                    if (Enum.IsDefined(e, hash) == true)
                    {
                        return Enum.GetName(e, hash) + " defined as " + hash.ToString("X16");
                    }
                }
            }

            return hash.ToString("X16");
        }
    }
}
