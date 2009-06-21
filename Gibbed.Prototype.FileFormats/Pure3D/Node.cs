using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    public abstract class Node
    {
        public List<Node> Children = new List<Node>();

        public abstract void Serialize(Stream output);
        public abstract void Deserialize(Stream input);

        public virtual System.Drawing.Image Preview()
        {
            return null;
        }
    }
}
