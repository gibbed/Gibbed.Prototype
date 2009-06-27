using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Track
{
    [KnownTrack(typeof(Context.Scenario), "mixer")]
    public class Mixer : TrackBase
    {
        public float TimeBegin;
        public float FadeTime;
        public UInt64 MixerHash;
        public UInt64 CategoryHash;
        public bool FadeIn;
        public bool UninstallOnExit;

        public override void SerializeProperties(Stream input, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeProperties(Stream input, FightFile fight)
        {
            this.TimeBegin = input.ReadF32();
            this.FadeTime = input.ReadF32();
            this.MixerHash = fight.ReadNameHash(input);
            this.CategoryHash = fight.ReadNameHash(input);
            this.FadeIn = input.ReadU32() == 0 ? false : true;
            this.UninstallOnExit = input.ReadU32() == 0 ? false : true;
        }
    }
}
