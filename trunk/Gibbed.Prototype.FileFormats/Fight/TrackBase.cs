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
    public abstract class TrackBase
    {
        public UInt32 Slave;
        public UInt64 UnknownHash;

        public void Serialize(Stream output, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public abstract void SerializeProperties(Stream input, FightFile fight);

        public void Deserialize(Stream input, FightFile fight)
        {
            this.Slave = input.ReadValueU32();
        }

        public abstract void DeserializeProperties(Stream input, FightFile fight);

        private static TrackBase DeserializeTrack(UInt64 hash, Stream input, FightFile fight)
        {
            Type type = TrackCache.GetType(fight.Context.GetType(), hash);
            if (type == null)
            {
                throw new InvalidOperationException("unknown track type (" + FightHashes.Lookup(hash) + ")");
            }

            UInt32 unknown = input.ReadValueU32();

            TrackBase track;

            try
            {
                track = (TrackBase)System.Activator.CreateInstance(type);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }

            track.Deserialize(input, fight);
            track.DeserializeProperties(input, fight);
            track.UnknownHash = input.ReadValueU64();

            return track;
        }

        public static List<TrackBase> DeserializeTracks(string name, Stream input, FightFile fight)
        {
            if (input.ReadValueU64() != name.Hash1003F())
            {
                throw new Exception();
            }

            if (input.ReadValueU32() != 0)
            {
                throw new Exception();
            }

            List<TrackBase> tracks = new List<TrackBase>();

            while (true)
            {
                UInt64 hash = fight.ReadHash(input);
                if (hash == 0)
                {
                    break;
                }

                tracks.Add(DeserializeTrack(hash, input, fight));
            }

            return tracks;
        }
    }
}
