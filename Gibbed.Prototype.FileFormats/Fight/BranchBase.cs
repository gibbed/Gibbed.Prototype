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
        public UInt64 NameHash;
        public string Path;
        public UInt32 SiblingIndex;
        public List<BranchBase> Branches;

        public void Serialize(Stream output, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public abstract void SerializeProperties(Stream input, FightFile fight);

        public void Deserialize(Stream input, FightFile fight)
        {
            this.NameHash = fight.ReadHash(input);
            this.Path = input.ReadAlignedASCII();
            this.SiblingIndex = input.ReadU32();

            if ((fight.Flags & 1) == 0)
            {
                throw new Exception();
            }
        }

        public abstract void DeserializeProperties(Stream input, FightFile fight);

        private static BranchBase DeserializeBranch(UInt64 hash, Stream input, FightFile fight)
        {
            Type type = BranchCache.GetType(hash);
            if (type == null)
            {
                throw new InvalidOperationException("unknown branch type (" + FightHashes.Lookup(hash) + ")");
            }

            UInt32 length = input.ReadU32();
            //long current = input.Position;

            BranchBase branch;

            try
            {
                branch = (BranchBase)System.Activator.CreateInstance(type);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }

            branch.Deserialize(input, fight);

            /*
            wat :|
            if (input.Position != current + length)
            {
                throw new Exception();
            }
            */

            branch.DeserializeProperties(input, fight);

            branch.Branches = BranchBase.DeserializeBranches(input, fight);
            return branch;
        }

        public static List<BranchBase> DeserializeBranches(Stream input, FightFile fight)
        {
            List<BranchBase> branches = new List<BranchBase>();

            while (true)
            {
                UInt64 hash = fight.ReadHash(input);
                if (hash == 0)
                {
                    break;
                }

                branches.Add(DeserializeBranch(hash, input, fight));
            }

            return branches;
        }
    }
}
