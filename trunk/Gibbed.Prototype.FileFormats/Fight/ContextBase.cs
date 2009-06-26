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

        public virtual void Serialize(Stream output, FightFile parent)
        {
            throw new NotImplementedException();
        }

        private ConditionBase DeserializeCondition(BranchBase branch, UInt64 hash, Stream input, FightFile parent)
        {
            Type type = ConditionCache.GetCondition(branch.GetType(), hash);
            if (type == null)
            {
                throw new InvalidOperationException("unknown condition type (" + hash.ToString("X16") + ") for branch " + branch.GetType().Name);
            }

            UInt32 unknown = input.ReadU32();

            ConditionBase condition;

            try
            {
                condition = (ConditionBase)System.Activator.CreateInstance(type);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }

            condition.Deserialize(input, parent);

            return condition;
        }

        private BranchBase DeserializeBranch(UInt64 hash, Stream input, FightFile parent)
        {
            Type type = BranchCache.GetBranch(this.GetType(), hash);
            if (type == null)
            {
                throw new InvalidOperationException("unknown branch type (" + hash.ToString("X16") + ") for context " + this.GetType().Name);
            }

            UInt32 length = input.ReadU32();
            Stream stream = input.ReadToMemoryStream(length);

            BranchBase branch;

            try
            {
                branch = (BranchBase)System.Activator.CreateInstance(type);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }

            branch.Deserialize(input, parent);

            while (true)
            {
                UInt64 conditionHash = parent.ReadHash100F4(input);
                if (conditionHash == 0)
                {
                    break;
                }

                branch.Conditions.Add(this.DeserializeCondition(branch, conditionHash, input, parent));
            }

            return branch;
        }

        public virtual void Deserialize(Stream input, FightFile parent)
        {
            while (true)
            {
                UInt64 branchHash = parent.ReadHash100F4(input);
                if (branchHash == 0)
                {
                    break;
                }

                this.Branches.Add(this.DeserializeBranch(branchHash, input, parent));
            }
        }
    }
}
