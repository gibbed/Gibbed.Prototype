using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight.Track
{
    [KnownTrack(typeof(Context.Scenario), "execute")]
    public class Execute : TrackBase
    {
        public float TimeBegin;
        public float TimeEnd;
        public BranchReference Branch;
        public List<ConditionBase> Conditions;

        public override void SerializeProperties(Stream input, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeProperties(Stream input, FightFile fight)
        {
            this.TimeBegin = fight.ReadPropertyFloat(input);
            this.TimeEnd = fight.ReadPropertyFloat(input);
            this.Branch = fight.ReadPropertyBranch(input);
            this.Conditions = ConditionBase.DeserializeConditions("conditions", input, fight);
        }
    }
}
