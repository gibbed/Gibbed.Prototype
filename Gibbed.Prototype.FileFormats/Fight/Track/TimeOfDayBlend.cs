using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Track
{
    public enum TimeOfDayBlendLevel : ulong
    {
        PowersLevel = 0x16C9F06B6FAA3274, // "Powers Level"
        FXLevel = 0xBA6DFC619762B410, // "FX Level"
        AtmosphereLevel = 0x3C498EC08D113A76, // "Atmosphere Level"
    }

    [KnownTrack(typeof(Context.Scenario), "FX_TimeOfDayBlend")]
    public class TimeOfDayBlend : TrackBase
    {
        public UInt64 GroupNameHash;
        public UInt64 VariationNameHash;
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
