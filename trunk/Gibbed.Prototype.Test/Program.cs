using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using Gibbed.Helpers;
using Gibbed.Prototype.FileFormats;
using Gibbed.Prototype.Helpers;

namespace Gibbed.Prototype.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            //Stream input = File.OpenRead("..\\other\\testfiles\\e10m04.fig");
            //Stream input = File.OpenRead("..\\other\\testfiles\\1_state.fig");
            Stream input = File.OpenRead("test\\startup_fig\\common.fight");
            FightFile fight = new FightFile();
            fight.Deserialize(input);
            input.Close();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            Stream output = File.OpenWrite("e10m04.xml");
            XmlWriter writer = XmlWriter.Create(output, settings);

            NetDataContractSerializer serializer = new NetDataContractSerializer("prototype", "protospace");
            //serializer.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;

            serializer.WriteObject(writer, fight);
            writer.Flush();
            output.Close();
            */
             
            /*
            Stream input = File.OpenRead("test\\art\\art\\startup_fig.p3d");
            Pure3DFile pure = new Pure3DFile();
            pure.Deserialize(input);
            input.Close();

            foreach (Gibbed.Prototype.FileFormats.Pure3D.FightDefinition def in pure.Nodes)
            {
                Gibbed.Prototype.FileFormats.Pure3D.FightData data = (Gibbed.Prototype.FileFormats.Pure3D.FightData)def.Children.SingleOrDefault(candidate => candidate is Gibbed.Prototype.FileFormats.Pure3D.FightData);
                if (data == null)
                {
                    continue;
                }

                Stream output = File.Open("test\\startup_fig\\" + def.Name + ".fight", FileMode.Create, FileAccess.Write, FileShare.None);
                output.Write(data.Data, 0, data.Data.Length);
                output.Close();
            }
            */
        }
    }
}
