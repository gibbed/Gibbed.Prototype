using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight
{
    public abstract class ContextBase
    {
        public List<BranchBase> Branches;

        public virtual void Serialize(Stream output, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public virtual void Deserialize(Stream input, FightFile fight)
        {
            this.Branches = BranchBase.DeserializeBranches(input, fight);
        }
    }
}
