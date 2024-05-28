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
    public class CompilerConfigFile
    {
        /// <summary>
        /// Specifies the current instance of the class loaded in memory.
        /// </summary>
        public static CompilerConfigFile loaded = null;
        /// <summary>
        /// Specifies whether the logger will be enabled or not.
        /// </summary>
        public bool loggerEnabled { get; set; } = true;
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
        public string mainAppProjName { get; set; } = "";
        /// <summary>
        /// Specifies the class name where the Main method is located.
        /// </summary>
        public string mainMethodClassName { get; set; } = "Program";
        /// <summary>
        /// Specifies the name of the package of the app.
        /// </summary>
        public string packageName { get; set; } = "TestApp";
        /// <summary>
        /// Specifies if should log a message as Message Box when the Source Logger is disabled or connection is lost, instead of throwing an exception.
        /// </summary>
        public bool logAsMessageBoxWhenLoggerDisabled { get; set; } = false;

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
