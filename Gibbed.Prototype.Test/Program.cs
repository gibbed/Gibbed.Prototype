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
            //Stream input = File.OpenRead("T:\\Projects\\Prototype\\other\\dummy.p3d");
            //Stream input = File.OpenRead("T:\\Projects\\Prototype\\other\\e09m01_tod.p3d");
            Stream input = File.OpenRead("T:\\Projects\\Prototype\\other\\manhattanTest_fig.p3d");
            Pure3DFile p3d = new Pure3DFile();
            p3d.Deserialize(input);
            input.Close();
        }
    }
}
