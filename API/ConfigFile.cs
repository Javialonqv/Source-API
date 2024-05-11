using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace API
{
    internal class ConfigFile
    {
        public static ConfigFile loaded = null;
        public bool loggerEnabled = false;

        public ConfigFile() { }
        public ConfigFile(bool loggerEnabled)
        {
            this.loggerEnabled = loggerEnabled;
        }

        // Inits an instance of ConfigFile by reading a JSON to be used later.
        public static void Init(string jsonFilePath)
        {
            if (loaded == null)
            {
                string content = File.ReadAllText(jsonFilePath);
                ConfigFile config = new ConfigFile();
                config = JsonConvert.DeserializeObject<ConfigFile>(content);
                loaded = config;
            }
        }
    }
}
