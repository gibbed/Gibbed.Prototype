using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Track
{
    [KnownTrack(typeof(Context.PropLogic), "playback")]
    public class Playback : TrackBase
    {
        public float TimeBegin;
        public UInt64 StateHash;
        public UInt64 SpecificPlaybackSetHash;
        public bool NotifyOnPlaybackFinished;

        public override void SerializeProperties(Stream input, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeProperties(Stream input, FightFile fight)
        {
            this.TimeBegin = input.ReadF32();
            this.StateHash = input.ReadU64();
            this.SpecificPlaybackSetHash = input.ReadU64();
            this.NotifyOnPlaybackFinished = input.ReadU32() == 0 ? false : true;
        }
    }
}
