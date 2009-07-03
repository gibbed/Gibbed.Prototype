using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Track
{
    public enum MusicPriority : ulong
    {
        Global = 0xA7E0F56B3A035591,
        Mission = 0xB677BAA57144F81E,
    }

    [KnownTrack(typeof(Context.Scenario), "music")]
    public class Music : TrackBase
    {
        public float TimeBegin;
        public UInt64 GroupHash;
        public UInt64 CueHash;
        public UInt64 PartHash;

        public MusicPriority Priority;
        public bool ResetPriority;
        
        public bool OverrideFadeOut;
        public int FadeoutStartBar;
        public int FadeoutStartBeat;
        public int FadeoutStartNote;
        public int FadeoutLengthBar;
        public int FadeoutLengthBeat;
        public int FadeoutLengthNote;
        
        public bool OverrideFadeIn;
        public int FadeinStartBar;
        public int FadeinStartBeat;
        public int FadeinStartNote;
        public int FadeinLengthBar;
        public int FadeinLengthBeat;
        public int FadeinLengthNote;

        public override void SerializeProperties(Stream input, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeProperties(Stream input, FightFile fight)
        {
            this.TimeBegin = fight.ReadPropertyFloat(input);
            this.GroupHash = fight.ReadPropertyName(input);
            this.CueHash = fight.ReadPropertyName(input);
            this.PartHash = fight.ReadPropertyName(input);
            this.Priority = fight.ReadPropertyEnum<MusicPriority>(input);
            this.ResetPriority = fight.ReadPropertyBool(input);

            this.OverrideFadeOut = fight.ReadPropertyBool(input);
            this.FadeoutStartBar = fight.ReadPropertyInt(input);
            this.FadeoutStartBeat = fight.ReadPropertyInt(input);
            this.FadeoutStartNote = fight.ReadPropertyInt(input);
            this.FadeoutLengthBar = fight.ReadPropertyInt(input);
            this.FadeoutLengthBeat = fight.ReadPropertyInt(input);
            this.FadeoutLengthNote = fight.ReadPropertyInt(input);

            this.OverrideFadeIn = fight.ReadPropertyBool(input);
            this.FadeinStartBar = fight.ReadPropertyInt(input);
            this.FadeinStartBeat = fight.ReadPropertyInt(input);
            this.FadeinStartNote = fight.ReadPropertyInt(input);
            this.FadeinLengthBar = fight.ReadPropertyInt(input);
            this.FadeinLengthBeat = fight.ReadPropertyInt(input);
            this.FadeinLengthNote = fight.ReadPropertyInt(input);
        }
    }
}
