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
    public enum MusicPriority : ulong
    {
        Global = 0xA7E0F56B3A035591,
        Mission = 0xB677BAA57144F81E,
    }

    [KnownTrack(typeof(Context.Scenario), "music")]
    public class Music : TrackBase
    {
        public float TimeBegin;
        public ulong GroupHash;
        public ulong CueHash;
        public ulong PartHash;

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
