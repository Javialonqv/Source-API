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

        public static void CreateSRCFile(CompilerConfigFile configFile, string srcFilePath)
        {
            FileStream fs = new FileStream(srcFilePath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            Dictionary<string, object> toSerialize = new Dictionary<string, object>();
            toSerialize.Add("loggerEnabled", configFile.loggerEnabled);
            toSerialize.Add("mainAppProjName", configFile.mainAppProjName);
            bf.Serialize(fs, toSerialize);
        }
    }
}
