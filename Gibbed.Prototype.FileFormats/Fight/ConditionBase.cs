using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gibbed.Prototype.FileFormats.Fight
{
    public abstract class ConditionBase
    {
        public abstract void Serialize(Stream output, FightFile parent);
        public abstract void Deserialize(Stream input, FightFile parent);
    }
}
