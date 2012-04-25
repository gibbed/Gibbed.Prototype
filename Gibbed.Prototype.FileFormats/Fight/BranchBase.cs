/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats.Fight
{
    // +0x34
    public abstract class BranchBase
    {
        public ulong NameHash;
        public string Path;
        public uint SiblingIndex;
        public List<BranchBase> Branches;

        public void Serialize(Stream output, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public abstract void SerializeProperties(Stream input, FightFile fight);

        public void Deserialize(Stream input, FightFile fight)
        {
            this.NameHash = fight.ReadHash(input);
            this.Path = input.ReadStringAlignedASCII();
            this.SiblingIndex = input.ReadValueU32();

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

            var length = input.ReadValueU32();
            //long current = input.Position;

            BranchBase branch;

            try
            {
                branch = (BranchBase)Activator.CreateInstance(type);
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

            branch.Branches = DeserializeBranches(input, fight);
            return branch;
        }

        public static List<BranchBase> DeserializeBranches(Stream input, FightFile fight)
        {
            var branches = new List<BranchBase>();

            while (true)
            {
                var hash = fight.ReadHash(input);
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
