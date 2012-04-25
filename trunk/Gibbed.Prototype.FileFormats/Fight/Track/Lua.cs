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
using System.IO;
using System.Text;
using Gibbed.IO;

namespace Gibbed.Prototype.FileFormats.Fight.Track
{
    [KnownTrack(typeof(Context.Scenario), "lua")]
    public class Lua : TrackBase
    {
        public float TimeBegin;
        public float TimeEnd;
        public byte[] Script;

        public string ScriptText
        {
            get
            {
                if (this.Script.Length < 5)
                {
                    return Encoding.ASCII.GetString(this.Script);
                }

                if (this.Script[0] == 0x1B &&
                    this.Script[1] == 0x4C &&
                    this.Script[2] == 0x75 &&
                    this.Script[3] == 0x61 &&
                    this.Script[4] == 0x51)
                {
                    return null;
                }

                return Encoding.ASCII.GetString(this.Script);
            }
        }

        public override void SerializeProperties(Stream input, FightFile fight)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeProperties(Stream input, FightFile fight)
        {
            this.TimeBegin = fight.ReadPropertyFloat(input);
            this.TimeEnd = fight.ReadPropertyFloat(input);

            int length = input.ReadValueS32();
            this.Script = new byte[length];
            input.ReadAligned(this.Script, 0, length, 4);
        }
    }
}
