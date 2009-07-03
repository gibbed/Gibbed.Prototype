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
            this.TimeBegin = fight.ReadPropertyFloat(input);
            this.FadeTime = fight.ReadPropertyFloat(input);
            this.MixerHash = fight.ReadPropertyName(input);
            this.CategoryHash = fight.ReadPropertyName(input);
            this.FadeIn = fight.ReadPropertyBool(input);
            this.UninstallOnExit = fight.ReadPropertyBool(input);
        }
    }
}
