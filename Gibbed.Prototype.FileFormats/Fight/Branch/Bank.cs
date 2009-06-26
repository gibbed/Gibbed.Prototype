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
    [KnownBranch(typeof(Context.PropLogic), "bank")]
    public class Bank : BranchBase
    {
        public UInt64 NameHash;
        public string Path;
        public UInt32 SiblingIndex;

        public override void Serialize(Stream output, FightFile parent)
        {
            throw new NotImplementedException();
        }

        public override void Deserialize(Stream input, FightFile parent)
        {
            this.NameHash = parent.ReadHash100F4(input);
            this.Path = input.ReadAlignedASCII();
            this.SiblingIndex = input.ReadU32();

            if ((parent.Flags & 1) == 0)
            {
                throw new Exception();
            }

            throw new NotImplementedException();
        }
    }
}
