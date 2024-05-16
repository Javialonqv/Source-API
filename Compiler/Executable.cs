using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Compiler
{
    [Serializable]
    internal class Executable
    {
        public CompilerConfigFile configFile;
        public string startProjName;

        public static void CreateSRCFile(CompilerConfigFile configFile, string srcFilePath)
        {
            Executable exe = new Executable();
            exe.configFile = configFile;

            FileStream fs = new FileStream(srcFilePath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, exe);
        }
    }
}
