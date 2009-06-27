﻿using System;
using Gibbed.Helpers;

namespace Gibbed.Prototype.FileFormats.Fight
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class KnownConditionAttribute : KnownHashForContextAttribute
    {
        public KnownConditionAttribute(Type type, UInt64 hash)
            : base(type, hash)
        {
        }

        public KnownConditionAttribute(Type type, string name)
            : base(type, name)
        {
        }
    }
}
