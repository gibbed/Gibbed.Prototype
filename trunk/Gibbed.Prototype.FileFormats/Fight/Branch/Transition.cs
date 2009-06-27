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
    [KnownBranch("transition")]
    public class Transition : BranchBase
    {
        public string ToState;
        public UInt32 Unknown;
        public List<ConditionBase> Conditions;

        public override void SerializeProperties(Stream input, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeProperties(Stream input, FightFile fight)
        {
            this.ToState = input.ReadAlignedASCII();

            if (this.ToState == null)
            {
                this.Unknown = input.ReadU32();
            }

            this.Conditions = ConditionBase.DeserializeConditions("conditions", input, fight);
        }
    }
}
