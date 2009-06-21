using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Gibbed.Helpers;
using Gibbed.Prototype.Helpers;
using Gibbed.Prototype.FileFormats;

namespace Gibbed.Prototype.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream input = File.OpenRead("T:\\Games\\Singleplayer\\Prototype\\scripts.rcf");
            CementFile cement = new CementFile();
            cement.Deserialize(input);
            input.Close();
        }
    }
}
