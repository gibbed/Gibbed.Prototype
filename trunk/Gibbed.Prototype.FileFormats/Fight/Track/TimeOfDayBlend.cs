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
    public enum TimeOfDayBlendLevel : ulong
    {
        // ReSharper disable InconsistentNaming
        PowersLevel = 0x16C9F06B6FAA3274, // "Powers Level"
        FXLevel = 0xBA6DFC619762B410, // "FX Level"
        AtmosphereLevel = 0x3C498EC08D113A76, // "Atmosphere Level"
        // ReSharper restore InconsistentNaming
    }

    [KnownTrack(typeof(Context.Scenario), "FX_TimeOfDayBlend")]
    public class TimeOfDayBlend : TrackBase
    {
        public ulong GroupNameHash;
        public ulong VariationNameHash;
        public float FadeIn;
        public float Duration;
        public float FadeOut;
        public TimeOfDayBlendLevel Level;
        public bool Sky;
        public bool GlobalLighting;
        public bool Lighting;
        public bool Bloom;
        public bool ColourMatrix;
        public bool Shaders;
        public bool Clouds;
        public bool Fog;
        public bool Abortable;
        public bool DynamicDuration;
        public float TimeBegin;
        public float TimeEnd;

        public override void SerializeProperties(Stream input, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeProperties(Stream input, FightFile fight)
        {
            this.GroupNameHash = fight.ReadPropertyName(input);
            this.VariationNameHash = fight.ReadPropertyName(input);
            this.FadeIn = fight.ReadPropertyFloat(input);
            this.Duration = fight.ReadPropertyFloat(input);
            this.FadeOut = fight.ReadPropertyFloat(input);
            this.Level = fight.ReadPropertyEnum<TimeOfDayBlendLevel>(input);
            this.Sky = fight.ReadPropertyBool(input);
            this.GlobalLighting = fight.ReadPropertyBool(input);
            this.Lighting = fight.ReadPropertyBool(input);
            this.Bloom = fight.ReadPropertyBool(input);
            this.ColourMatrix = fight.ReadPropertyBool(input);
            this.Shaders = fight.ReadPropertyBool(input);
            this.Clouds = fight.ReadPropertyBool(input);
            this.Fog = fight.ReadPropertyBool(input);
            this.Abortable = fight.ReadPropertyBool(input);
            this.DynamicDuration = fight.ReadPropertyBool(input);
            this.TimeBegin = fight.ReadPropertyFloat(input);
            this.TimeEnd = fight.ReadPropertyFloat(input);
        }
    }
}
