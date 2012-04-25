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
using System.IO;
using System.Reflection;
using System.Text;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats
{
    public class FightFile
    {
        public UInt64 ReadHash(Stream stream)
        {
            if (this.HashesArePrecalculated == true)
            {
                return stream.ReadValueU64();
            }

            throw new InvalidOperationException("unsure how to handle this");
            // suspected that this is a uint32 + string rather than the hash
            return stream.ReadString(stream.ReadValueU32(), Encoding.ASCII).Hash1003F();
        }

        public T ReadPropertyEnum<T>(Stream stream)
        {
            UInt64 value = stream.ReadValueU64();
            if (Enum.IsDefined(typeof(T), value) == false)
            {
                throw new Exception(FightHashes.Lookup(value));
            }
            return (T)Enum.ToObject(typeof(T), value);
        }

        public UInt64 ReadPropertyName(Stream stream)
        {
            return this.ReadHash(stream);
        }

        public string ReadPropertyString(Stream stream)
        {
            return stream.ReadStringAlignedASCII();
        }

        public int ReadPropertyInt(Stream stream)
        {
            return stream.ReadValueS32();
        }

        public float ReadPropertyFloat(Stream stream)
        {
            return stream.ReadValueF32();
        }

        public bool ReadPropertyBool(Stream stream)
        {
            return stream.ReadValueB32();
        }

        public Fight.BranchReference ReadPropertyBranch(Stream stream)
        {
            // ReSharper disable UseObjectOrCollectionInitializer
            var rez = new Fight.BranchReference();
            // ReSharper restore UseObjectOrCollectionInitializer

            rez.Name = stream.ReadStringAlignedASCII();

            if (string.IsNullOrEmpty(rez.Name) == true)
            {
                rez.Index = stream.ReadValueU32();
            }

            return rez;
        }

        public UInt32 Flags;

        #region public bool HashesArePrecalculated
        public bool HashesArePrecalculated
        {
            get
            {
                if ((this.Flags & 4) == 4)
                {
                    return true;
                }

                return false;
            }
        }
        #endregion

        public UInt32 Unknown1;
        public UInt32 Unknown2;
        public UInt32 Unknown3;
        public UInt64 NameHash; // should be the hash of FightDefinition.Name (from P3D)
        public string Path;
        public UInt32 Unknown4;

        public Fight.ContextBase Context;

        public void Serialize(Stream output)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Stream input)
        {
            if (input.ReadString(4, Encoding.ASCII) != "fig0")
            {
                throw new FormatException("not a fight file");
            }

            this.Flags = input.ReadValueU32();
            this.Unknown1 = input.ReadValueU32();

            if (input.ReadValueU64() != (UInt64)FightHash.Chunk)
            {
                throw new FormatException("containing fight type is not chunk");
            }

            this.Unknown2 = input.ReadValueU32();
            this.Unknown3 = input.ReadValueU32();

            this.NameHash = this.ReadHash(input);

            UInt64 contextHash = this.ReadHash(input);

            this.Path = input.ReadStringAlignedASCII();
            this.Unknown4 = input.ReadValueU32();

            if ((this.Flags & 1) == 0)
            {
                throw new Exception();
            }

            Type contextType = Fight.ContextCache.GetContext(contextHash);
            if (contextType == null)
            {
                throw new InvalidOperationException("unknown context type (" + FightHashes.Lookup(contextHash) + ")");
            }

            try
            {
                this.Context = (Fight.ContextBase)Activator.CreateInstance(contextType);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }

            this.Context.Deserialize(input, this);
        }
    }
}
