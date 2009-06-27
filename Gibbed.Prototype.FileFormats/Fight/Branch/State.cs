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
    [KnownBranch("state")]
    public class State : BranchBase
    {
        public List<TrackBase> EnterTracks;
        public List<TrackBase> ExitTracks;

        public override void SerializeProperties(Stream input, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeProperties(Stream input, FightFile fight)
        {
            this.EnterTracks = TrackBase.DeserializeTracks("enterTracks", input, fight);
            this.ExitTracks = TrackBase.DeserializeTracks("exitTracks", input, fight);
        }
    }
}
