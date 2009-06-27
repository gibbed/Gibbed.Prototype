using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Track
{
    [KnownTrack(typeof(Context.Scenario), "FMV")]
    [KnownTrack(typeof(Context.PropLogic), "FMV")]
    public class FMV : TrackBase
    {
        public float TimeBegin;
        public float TimeEnd;
        public string Filename;
        public bool UseFMVState;
        public bool ClearSubtitles;
        public bool IsPreLoaded;
        public bool UseBlackFades;
        public bool FadeInOnEnter;
        public bool StayFadedOnExit;
        public bool FadeUpOnExit;
        public bool DelayUninstall;
        public float UninstallDelayTime;
        public float WhiteFadeTime;
        public float Timer;

        public override void SerializeProperties(Stream input, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeProperties(Stream input, FightFile fight)
        {
            this.TimeBegin = input.ReadF32();
            this.TimeEnd = input.ReadF32();
            this.Filename = input.ReadAlignedASCII();
            this.UseFMVState = input.ReadU32() == 0 ? false : true;
            this.ClearSubtitles = input.ReadU32() == 0 ? false : true;
            this.IsPreLoaded = input.ReadU32() == 0 ? false : true;
            this.UseBlackFades = input.ReadU32() == 0 ? false : true;
            this.FadeInOnEnter = input.ReadU32() == 0 ? false : true;
            this.StayFadedOnExit = input.ReadU32() == 0 ? false : true;
            this.FadeUpOnExit = input.ReadU32() == 0 ? false : true;
            this.DelayUninstall = input.ReadU32() == 0 ? false : true;
            this.UninstallDelayTime = input.ReadF32();
            this.WhiteFadeTime = input.ReadF32();
            this.Timer = input.ReadF32();
        }
    }
}
