using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Gibbed.Helpers;

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

            output.WriteU32(node.TypeId);
            output.WriteU32((UInt32)(12 + nodeStream.Length));
            output.WriteU32((UInt32)(12 + nodeStream.Length + childrenStream.Length));

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

            output.WriteU32(0xFF443350);
            output.WriteU32(12);
            output.WriteU32((UInt32)(12 + nodesStream.Length));
            nodesStream.Seek(0, SeekOrigin.Begin);
            output.WriteFromStream(nodesStream, nodesStream.Length);
        }

        #region TypeCache
        // Lame ass way to do this but, it'll work for now.
        private static class TypeCache
        {
            private static Dictionary<UInt32, Type> Lookup = null;

            private static void BuildLookup()
            {
                Lookup = new Dictionary<uint, Type>();

                foreach (Type type in Assembly.GetAssembly(typeof(TypeCache)).GetTypes())
                {
                    if (type.IsSubclassOf(typeof(Pure3D.BaseNode)) == true)
                    {
                        object[] attributes = type.GetCustomAttributes(typeof(Pure3D.KnownTypeAttribute), false);
                        if (attributes.Length == 1)
                        {
                            Pure3D.KnownTypeAttribute attribute = (Pure3D.KnownTypeAttribute)attributes[0];
                            Lookup.Add(attribute.Id, type);
                        }
                    }
                }
            }

            public static Type GetType(UInt32 typeId)
            {
                if (Lookup == null)
                {
                    BuildLookup();
                }

                if (Lookup.ContainsKey(typeId))
                {
                    return Lookup[typeId];
                }

                return null;
            }
        }
        #endregion

        private Pure3D.BaseNode DeserializeNode(Stream input)
        {
            UInt32 typeId = input.ReadU32();
            UInt32 thisSize = input.ReadU32() - 12;
            UInt32 childrenSize = input.ReadU32() - thisSize - 12;

            Pure3D.BaseNode node;
            Type type = TypeCache.GetType(typeId);

            if (type != null)
            {
                try
                {
                    node = (Pure3D.BaseNode)System.Activator.CreateInstance(type);
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

            Stream nodeStream = input.ReadToMemoryStream(thisSize);
            Stream childrenStream = input.ReadToMemoryStream(childrenSize);

            while (childrenStream.Position < childrenStream.Length)
            {
                node.Children.Add(this.DeserializeNode(childrenStream));
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
            UInt32 magic = input.ReadU32();

            if (magic == 0x503344FF)
            {
                throw new FormatException("no support for big-endian Pure3D files");
            }
            else if (magic != 0xFF443350)
            {
                throw new FormatException("not a Pure3D file");
            }

            UInt32 headerSize = input.ReadU32();
            if (headerSize != 12)
            {
                throw new FormatException("invalid header size");
            }

            UInt32 dataSize = input.ReadU32();

            Stream nodesStream = input.ReadToMemoryStream(dataSize - 12);
            this.Nodes = new List<Pure3D.BaseNode>();
            while (nodesStream.Position < nodesStream.Length)
            {
                this.Nodes.Add(this.DeserializeNode(nodesStream));
            }
        }
    }
}
