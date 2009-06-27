using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Track
{
    [KnownTrack(typeof(Context.Scenario), "stateMachine_Transition")]
    public class StateMachineTransition : TrackBase
    {
        public float TimeBegin;
        public float TimeEnd;
        public string TransitionBranch;
        public UInt32 Unknown;

        public override void SerializeProperties(Stream input, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeProperties(Stream input, FightFile fight)
        {
            this.TimeBegin = input.ReadF32();
            this.TimeEnd = input.ReadF32();
            this.TransitionBranch = input.ReadAlignedASCII();
            if (this.TransitionBranch == null)
            {
                this.Unknown = input.ReadU32();
            }
        }
    }
}
