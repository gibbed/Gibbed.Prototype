using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbed.Prototype.FileFormats.Fight
{
    public abstract class KnownHashForContextAttribute : KnownHashAttribute
    {
        public Type Type;

        private void SetContextType(Type type)
        {
            if (type.IsSubclassOf(typeof(ContextBase)) == false)
            {
                throw new Exception();
            }

            this.Type = type;
        }

        public KnownHashForContextAttribute(Type type, UInt64 hash)
            : base(hash)
        {
            this.SetContextType(type);
        }

        public KnownHashForContextAttribute(Type type, string name)
            : base(name)
        {
            this.SetContextType(type);
        }
    }
}
