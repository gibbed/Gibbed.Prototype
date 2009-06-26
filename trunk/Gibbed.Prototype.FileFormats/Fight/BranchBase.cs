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
    // +0x34

    public abstract class BranchBase
    {
        public List<ConditionBase> Conditions;

        public abstract void Serialize(Stream output, FightFile parent);
        public abstract void Deserialize(Stream input, FightFile parent);
    }
}
