using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Gibbed.Helpers;
using Gibbed.Prototype.FileFormats;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream input = File.OpenRead("..\\other\\testfiles\\e10m04.fig");
            //Stream input = File.OpenRead("..\\other\\testfiles\\1_state.fig");
            FightFile fight = new FightFile();
            fight.Deserialize(input);
            input.Close();
        }
    }
}
