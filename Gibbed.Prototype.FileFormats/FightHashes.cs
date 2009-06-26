using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbed.Prototype.FileFormats
{
    public static class FightHashes
    {
        public const UInt64 Chunk = 0x616FE21D386330FF; // "chunk"
        public const UInt64 Bank = 0x006248CCFF4DC4FA; // "bank"
        public const UInt64 Node = 0x006E51B53282267C; // "node"
        public const UInt64 Store = 0x7149C22710BC52F3; // "store"
        public const UInt64 Reference = 0x87E400FE4359E3E7; // "reference"
    }
}
