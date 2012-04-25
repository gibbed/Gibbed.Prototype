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

namespace Gibbed.Prototype.FileFormats
{
    public class Pure3DFile
    {
        public List<Pure3D.BaseNode> Nodes;

        private void SerializeNode(Stream output, Pure3D.BaseNode node)
        {
            Stream childrenStream = new MemoryStream();
            foreach (Pure3D.BaseNode child in node.Children)
            {
                this.SerializeNode(childrenStream, child);
            }

            Stream nodeStream = new MemoryStream();
            node.Serialize(nodeStream);

            output.WriteValueU32(node.TypeId);
            output.WriteValueU32((UInt32)(12 + nodeStream.Length));
            output.WriteValueU32((UInt32)(12 + nodeStream.Length + childrenStream.Length));

            nodeStream.Seek(0, SeekOrigin.Begin);
            output.WriteFromStream(nodeStream, nodeStream.Length);

            childrenStream.Seek(0, SeekOrigin.Begin);
            output.WriteFromStream(childrenStream, childrenStream.Length);
        }

        public void Serialize(Stream output)
        {
            Stream nodesStream = new MemoryStream();
            foreach (Pure3D.BaseNode node in this.Nodes)
            {
                this.SerializeNode(nodesStream, node);
            }

            output.WriteValueU32(0xFF443350);
            output.WriteValueU32(12);
            output.WriteValueU32((UInt32)(12 + nodesStream.Length));
            nodesStream.Seek(0, SeekOrigin.Begin);
            output.WriteFromStream(nodesStream, nodesStream.Length);
        }

        #region TypeCache
        // Lame ass way to do this but, it'll work for now.
        private static class TypeCache
        {
            private static Dictionary<UInt32, Type> _Lookup;

            private static void BuildLookup()
            {
                _Lookup = new Dictionary<uint, Type>();

                foreach (Type type in Assembly.GetAssembly(typeof(TypeCache)).GetTypes())
                {
                    if (type.IsSubclassOf(typeof(Pure3D.BaseNode)) == true)
                    {
                        object[] attributes = type.GetCustomAttributes(typeof(Pure3D.KnownTypeAttribute), false);
                        if (attributes.Length == 1)
                        {
                            var attribute = (Pure3D.KnownTypeAttribute)attributes[0];
                            _Lookup.Add(attribute.Id, type);
                        }
                    }
                }
            }

            public static Type GetType(UInt32 typeId)
            {
                if (_Lookup == null)
                {
                    BuildLookup();
                }

                if (_Lookup != null && _Lookup.ContainsKey(typeId))
                {
                    return _Lookup[typeId];
                }

                return null;
            }
        }
        #endregion

        private Pure3D.BaseNode DeserializeNode(Stream input, Pure3D.BaseNode parent)
        {
            UInt32 typeId = input.ReadValueU32();
            UInt32 thisSize = input.ReadValueU32() - 12;
            UInt32 childrenSize = input.ReadValueU32() - thisSize - 12;

            Pure3D.BaseNode node;
            Type type = TypeCache.GetType(typeId);

            if (type != null)
            {
                try
                {
                    node = (Pure3D.BaseNode)Activator.CreateInstance(type);
                }
                catch (TargetInvocationException e)
                {
                    throw e.InnerException;
                }
            }
            else
            {
                node = new Pure3D.Unknown(typeId);
            }

            node.ParentNode = parent;
            node.ParentFile = this;

            Stream nodeStream = input.ReadToMemoryStream(thisSize);
            Stream childrenStream = input.ReadToMemoryStream(childrenSize);

            while (childrenStream.Position < childrenStream.Length)
            {
                Pure3D.BaseNode childNode = this.DeserializeNode(childrenStream, node);
                node.Children.Add(childNode);
            }

            node.Deserialize(nodeStream);

            if (nodeStream.Position != nodeStream.Length)
            {
                throw new Exception();
            }

            return node;
        }

        public void Deserialize(Stream input)
        {
            UInt32 magic = input.ReadValueU32();

            if (magic == 0x503344FF)
            {
                throw new FormatException("no support for big-endian Pure3D files");
            }

            if (magic != 0xFF443350)
            {
                throw new FormatException("not a Pure3D file");
            }

            UInt32 headerSize = input.ReadValueU32();
            if (headerSize != 12)
            {
                throw new FormatException("invalid header size");
            }

            UInt32 dataSize = input.ReadValueU32();

            Stream nodesStream = input.ReadToMemoryStream(dataSize - 12);
            this.Nodes = new List<Pure3D.BaseNode>();
            while (nodesStream.Position < nodesStream.Length)
            {
                this.Nodes.Add(this.DeserializeNode(nodesStream, null));
            }
        }
    }
}
