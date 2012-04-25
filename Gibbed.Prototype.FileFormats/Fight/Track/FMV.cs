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
using System.IO;

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
