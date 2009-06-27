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
    [KnownBranch("node")]
    public class Node : BranchBase
    {
        public bool Override;
        public List<ConditionBase> Conditions;
        public List<TrackBase> Tracks;

        public override void SerializeProperties(Stream input, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeProperties(Stream input, FightFile fight)
        {
            this.Override = input.ReadU32() == 0 ? false : true;
            this.Conditions = ConditionBase.DeserializeConditions("conditions", input, fight);
            this.Tracks = TrackBase.DeserializeTracks("tracks", input, fight);
        }
    }
}
