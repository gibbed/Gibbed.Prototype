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
        public Endian Endian;
        public List<Pure3D.BaseNode> Nodes = new List<Pure3D.BaseNode>();

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

        private static Pure3D.BaseNode DeserializeNode(Stream input, Endian endian, Pure3D.BaseNode parent)
        {
            var start = input.Position;

            var typeId = input.ReadValueU32(endian);
            var headerSize = input.ReadValueU32(endian);
            var totalSize = input.ReadValueU32(endian);

            var current = Pure3D.NodeFactory.CreateNode(typeId);
            
            if (current != null)
            {
                current.Deserialize(input);
            }
            else
            {
                var unknown = new Pure3D.Unknown(typeId);
                unknown.Data = input.ReadBytes(headerSize - 12);
                current = unknown;
            }

            current.ParentNode = parent;

            if (input.Position != start + headerSize)
            {
                throw new FormatException();
            }

            var end = start + totalSize;

            while (input.Position < end)
            {
                var child = DeserializeNode(input, endian, current);
                current.Children.Add(child);
            }

            if (input.Position != end)
            {
                throw new FormatException();
            }

            return current;
        }

        public const uint Signature = 0xFF443350; // 'P3D\xFF'

        public void Deserialize(Stream input)
        {
            var start = input.Position;

            var magic = input.ReadValueU32(Endian.Little);
            if (magic != Signature &&
                magic.Swap() != Signature)
            {
                throw new FormatException("not a Pure3D file");
            }
            var endian = magic == Signature ? Endian.Little : Endian.Big;

            var headerSize = input.ReadValueU32(endian);
            if (headerSize != 12)
            {
                throw new FormatException("invalid header size");
            }

            var totalSize = input.ReadValueU32(endian);
            if (start + totalSize > input.Length)
            {
                throw new FormatException();
            }

            var end = start + totalSize;

            this.Nodes.Clear();
            while (input.Position < end)
            {
                this.Nodes.Add(DeserializeNode(input, endian, null));
            }

            if (input.Position != end)
            {
                throw new FormatException();
            }

            this.Endian = endian;
        }
    }
}
