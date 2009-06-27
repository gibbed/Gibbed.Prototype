using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gibbed.Helpers;

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
        private static Dictionary<UInt64, string> RegisteredLookup = null;
        private static List<Type> RegisteredEnum = null;

        public static void Register(string text)
        {
            if (RegisteredLookup == null)
            {
                RegisteredLookup = new Dictionary<UInt64, string>();
            }

            RegisteredLookup[text.Hash1003F()] = text;
        }

        public static string Lookup(UInt64 hash)
        {
            if (RegisteredLookup != null && RegisteredLookup.ContainsKey(hash) == true)
            {
                return "\"" + RegisteredLookup[hash] + "\" == " + hash.ToString("X16");
            }

            if (RegisteredEnum == null)
            {
                RegisteredEnum = new List<Type>();
                RegisteredEnum.Add(typeof(Fight.Condition.ScenarioGameObjectSlot));
                RegisteredEnum.Add(typeof(Fight.Condition.ScenarioMessageType));
                RegisteredEnum.Add(typeof(Fight.Condition.EventWhenType));
                RegisteredEnum.Add(typeof(Fight.Track.MusicPriority));
                RegisteredEnum.Add(typeof(FightHash));
            }

            if (RegisteredEnum != null)
            {
                foreach (Type e in RegisteredEnum)
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
