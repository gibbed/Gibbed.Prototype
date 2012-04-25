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
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Gibbed.Prototype.FileFormats.Pure3D
{
    public abstract class BaseNode
    {
        #region public UInt32 TypeId;
        [DisplayName("Type ID")]
        [Category("Pure3D")]
        [TypeConverter(typeof(TypeIdConverter))]
        public virtual UInt32 TypeId
        {
            get
            {
                object[] attributes = this.GetType().GetCustomAttributes(typeof(KnownTypeAttribute), false);
                if (attributes.Length == 1)
                {
                    var attribute = (KnownTypeAttribute)attributes[0];
                    return attribute.Id;
                }

                return 0xFFFFFFFF;
            }
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public BaseNode ParentNode;
        public Pure3DFile ParentFile;
        public List<BaseNode> Children = new List<BaseNode>();

        #region public int ChildCount
        [DisplayName("Child Count")]
        [Category("Pure3D")]
        public int ChildCount
        {
            get
            {
                if (this.Children == null)
                {
                    return 0;
                }

                return this.Children.Count;
            }
        }
        #endregion

        public abstract void Serialize(Stream output);
        public abstract void Deserialize(Stream input);

        [Browsable(false)]
        public virtual bool Exportable
        {
            get { return false; }
        }

        public virtual void Export(Stream output)
        {
            throw new InvalidOperationException();
        }

        [Browsable(false)]
        public virtual bool Importable
        {
            get { return false; }
        }

        public virtual void Import(Stream input)
        {
            throw new InvalidOperationException();
        }

        public virtual object Preview()
        {
            return null;
        }

        public T GetParentNode<T>() where T : BaseNode
        {
            BaseNode node = this.ParentNode;

            if (node is T)
            {
                return node as T;
            }

            if (node != null)
            {
                return node.GetParentNode<T>();
            }

            return null;
        }

        public T GetChildNode<T>() where T : BaseNode
        {
            return (T)this.Children.SingleOrDefault(candidate => candidate is T);
        }

        public List<T> GetChildNodes<T>() where T : BaseNode
        {
            return new List<T>(this.Children.OfType<T>());
        }
    }
}
