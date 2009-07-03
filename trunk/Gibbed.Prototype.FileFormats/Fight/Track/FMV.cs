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
            this.TimeBegin = fight.ReadPropertyFloat(input);
            this.TimeEnd = fight.ReadPropertyFloat(input);
            this.Filename = fight.ReadPropertyString(input);
            this.UseFMVState = fight.ReadPropertyBool(input);
            this.ClearSubtitles = fight.ReadPropertyBool(input);
            this.IsPreLoaded = fight.ReadPropertyBool(input);
            this.UseBlackFades = fight.ReadPropertyBool(input);
            this.FadeInOnEnter = fight.ReadPropertyBool(input);
            this.StayFadedOnExit = fight.ReadPropertyBool(input);
            this.FadeUpOnExit = fight.ReadPropertyBool(input);
            this.DelayUninstall = fight.ReadPropertyBool(input);
            this.UninstallDelayTime = fight.ReadPropertyFloat(input);
            this.WhiteFadeTime = fight.ReadPropertyFloat(input);
            this.Timer = fight.ReadPropertyFloat(input);
        }
    }
}
