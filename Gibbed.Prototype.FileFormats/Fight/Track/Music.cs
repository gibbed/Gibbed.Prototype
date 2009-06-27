using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        public UInt32 FadeoutStartBar;
        public UInt32 FadeoutStartBeat;
        public UInt32 FadeoutStartNote;
        public UInt32 FadeoutLengthBar;
        public UInt32 FadeoutLengthBeat;
        public UInt32 FadeoutLengthNote;
        
        public bool OverrideFadeIn;
        public UInt32 FadeinStartBar;
        public UInt32 FadeinStartBeat;
        public UInt32 FadeinStartNote;
        public UInt32 FadeinLengthBar;
        public UInt32 FadeinLengthBeat;
        public UInt32 FadeinLengthNote;

        public override void SerializeProperties(Stream input, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeProperties(Stream input, FightFile fight)
        {
            this.TimeBegin = input.ReadF32();
            this.GroupHash = fight.ReadHash100F4(input);
            this.CueHash = fight.ReadHash100F4(input);
            this.PartHash = fight.ReadHash100F4(input);

            UInt64 priority = input.ReadU64();
            if (Enum.IsDefined(typeof(MusicPriority), priority) == false)
            {
                throw new Exception(FightHashes.Lookup(priority));
            }
            this.Priority = (MusicPriority)(priority);

            this.ResetPriority = input.ReadU32() == 0 ? false : true;

            this.OverrideFadeOut = input.ReadU32() == 0 ? false : true;
            this.FadeoutStartBar = input.ReadU32();
            this.FadeoutStartBeat = input.ReadU32();
            this.FadeoutStartNote = input.ReadU32();
            this.FadeoutLengthBar = input.ReadU32();
            this.FadeoutLengthBeat = input.ReadU32();
            this.FadeoutLengthNote = input.ReadU32();

            this.OverrideFadeIn = input.ReadU32() == 0 ? false : true;
            this.FadeinStartBar = input.ReadU32();
            this.FadeinStartBeat = input.ReadU32();
            this.FadeinStartNote = input.ReadU32();
            this.FadeinLengthBar = input.ReadU32();
            this.FadeinLengthBeat = input.ReadU32();
            this.FadeinLengthNote = input.ReadU32();
        }
    }
}
