using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Compiler
{
    /// <summary>
    /// Config class to save the Source API configurations.
    /// </summary>
    [Serializable]
    internal class CompilerConfigFile
    {
        /// <summary>
        /// Specifies the current instance of the class loaded in memory.
        /// </summary>
        public static CompilerConfigFile loaded = null;
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

        /// <summary>
        /// Inits an instance of the ConfigFile class by reading the SourceConfig.json file.
        /// </summary>
        /// <param name="jsonFilePath">The path of the SourceConfig.json file.</param>
        public static void Init(string jsonFilePath)
        {
            if (loaded == null) // This method only can be called when there's no JSON loaded yet.
            {
                string content = File.ReadAllText(jsonFilePath);
                CompilerConfigFile config = new CompilerConfigFile();
                // Set up the JSON reader to throw an exception if the JSON can't be readed.
                JsonSerializerSettings settings = new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Error };
                try
                {
                    config = JsonConvert.DeserializeObject<CompilerConfigFile>(content);
                    loaded = config;
                }
                catch
                {
                    //ExceptionsManager.CantDeserealizeSourceConfigFile(jsonFilePath);
                }
            }
        }
    }
}
