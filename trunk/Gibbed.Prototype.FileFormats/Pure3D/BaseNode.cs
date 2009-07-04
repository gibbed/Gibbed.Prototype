using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

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
                object[] attributes = this.GetType().GetCustomAttributes(typeof(Pure3D.KnownTypeAttribute), false);
                if (attributes.Length == 1)
                {
                    Pure3D.KnownTypeAttribute attribute = (Pure3D.KnownTypeAttribute)attributes[0];
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
        public virtual bool Exportable { get { return false; } }
        public virtual void Export(Stream output)
        {
            throw new InvalidOperationException();
        }

        [Browsable(false)]
        public virtual bool Importable { get { return false; } }
        public virtual void Import(Stream input)
        {
            throw new InvalidOperationException();
        }

        public virtual System.Drawing.Image Preview()
        {
            return null;
        }
    }
}
