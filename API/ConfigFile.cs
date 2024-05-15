using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace API
{
    /// <summary>
    /// Config class to save the Source API configurations.
    /// </summary>
    internal class ConfigFile
    {
        /// <summary>
        /// Specifies the current instance of the class loaded in memory.
        /// </summary>
        public static ConfigFile loaded = null;
        /// <summary>
        /// Specifies whether the logger will be enabled or not.
        /// </summary>
        public bool loggerEnabled = false;
        /// <summary>
        /// Specifies whether the logger will be enabled or not with exceptions control.
        /// </summary>
        public static bool LoggerEnabled
        {
            get
            {
                if (loaded == null) { return false; } // Returns a default value if there's no JSON loaded yet.
                else { return loaded.loggerEnabled; } // Otherwise, return the value of the JSON file.
            }
        }

        ConfigFile() { }
        ConfigFile(bool loggerEnabled)
        {
            this.loggerEnabled = loggerEnabled;
        }

        /// <summary>
        /// Inits an instance of the ConfigFile class by reading the SourceConfig.json file.
        /// </summary>
        /// <param name="jsonFilePath">The path of the SourceConfig.json file.</param>
        public static void Init(string jsonFilePath)
        {
            if (loaded == null) // This method only can be called when there's no JSON loaded yet.
            {
                string content = File.ReadAllText(jsonFilePath);
                ConfigFile config = new ConfigFile();
                // Set up the JSON reader to throw an exception if the JSON can't be readed.
                JsonSerializerSettings settings = new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Error };
                try
                {
                    config = JsonConvert.DeserializeObject<ConfigFile>(content);
                    loaded = config;
                }
                catch
                {
                    ExceptionsManager.CantDeserealizeSourceConfigFile(jsonFilePath);
                }
            }
        }
    }
}
