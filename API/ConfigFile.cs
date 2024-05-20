using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

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
        /// <summary>
        /// Specifies the main project where the SourceConfig.json file and Content folder are located.
        /// </summary>
        public string mainAppProjName = "";
        /// <summary>
        /// Specifies the class name where the Main method is located.
        /// </summary>
        public string mainMethodClassName = "";

        ConfigFile() { }
        ConfigFile(bool loggerEnabled)
        {
            this.loggerEnabled = loggerEnabled;
        }

        /// <summary>
        /// Inits an instance of the ConfigFile class by reading the SourceConfig.json file.
        /// </summary>
        /// <param name="jsonFilePath">The path of the SourceConfig.json file.</param>
        public static void Init(bool isCompiled, string jsonFilePath)
        {
            if (loaded == null) // This method only can be called when there's no JSON loaded yet.
            {
                if (!isCompiled)
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
                else
                {
                    // In this case, jsonFilePath is the path to the default.src file:
                    FileStream fs = new FileStream(jsonFilePath, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    Dictionary<string, object> deserialized = (Dictionary<string, object>)bf.Deserialize(fs);
                    ConfigFile config = new ConfigFile();
                    config.loggerEnabled = (bool)deserialized["loggerEnabled"];
                    config.mainAppProjName = (string)deserialized["mainAppProjName"];
                    config.mainMethodClassName = (string)deserialized["mainMethodClassName"];
                    loaded = config;
                }
            }
        }
    }
}
