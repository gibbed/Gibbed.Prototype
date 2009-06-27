using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Branch
{
    [KnownBranch("statemachine")]
    public class StateMachine : BranchBase
    {
        public List<TrackBase> InitialTracks;
        public List<TrackBase> Functions;
        public List<TrackBase> ExitTracks;

        public override void SerializeProperties(Stream input, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeProperties(Stream input, FightFile fight)
        {
            this.InitialTracks = TrackBase.DeserializeTracks("initialTracks", input, fight);
            this.Functions = TrackBase.DeserializeTracks("functions", input, fight);
            this.ExitTracks = TrackBase.DeserializeTracks("exitTracks", input, fight);
        }
    }
}
