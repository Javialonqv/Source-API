using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace API
{
    /// <summary>
    /// Handles all the paths required for the app execution.
    /// </summary>
    internal static class Paths
    {
        /// <summary>
        /// Specifies if the Paths class has been initialized.
        /// </summary>
        internal static bool initialized = false;

        /// <summary>
        /// Points to the SourceConfig.json file. This one only exists on debug mode.
        /// </summary>
        public static string sourceConfigFilePath = "";
        /// <summary>
        /// Points to the Logger.exe file.
        /// </summary>
        public static string loggerExecutableFilePath = "";
        /// <summary>
        /// Points to the "data" folder.
        /// </summary>
        public static string dataFolderPath = "";
        /// <summary>
        /// Points to the "Content" folder.
        /// </summary>
        public static string contentFolderPath = "";
        /// <summary>
        /// Points to the "resources.data" file.
        /// </summary>
        public static string resourcesDataFilePath = "";

        /// <summary>
        /// Inits the paths to be used during runtime.
        /// </summary>
        /// <param name="isCompiled">Specifies if the App is compiled with the Source Compiler.</param>
        /// <param name="srcFilePath">Specifies the path of the default.src file. Empty if isCompiled is false.</param>
        internal static void Init(bool isCompiled, string srcFilePath)
        {
            initialized = true;
            // If NOT compiled, get all the paths relative to the executing exe, otherwise, relative to the srcFilePath.
            if (!isCompiled)
            {
                sourceConfigFilePath = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                    "SourceConfig.json", SearchOption.AllDirectories)[0];
                loggerExecutableFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logger.exe");
                contentFolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                    "App", "Content");
                resourcesDataFilePath = "";
            }
            else
            {
                sourceConfigFilePath = srcFilePath;
                loggerExecutableFilePath = Path.Combine(Path.GetDirectoryName(srcFilePath), "data", "Logger.exe");
                contentFolderPath = Path.Combine(Path.GetDirectoryName(srcFilePath), "Content");
                resourcesDataFilePath = Path.Combine(contentFolderPath, "resources.data");
            }
            CheckDirectories(isCompiled);
        }

        /// <summary>
        /// Check if all the needed directories are created.
        /// </summary>
        /// <param name="isCompiled">Defines if the current app is already compiled.</param>
        static void CheckDirectories(bool isCompiled)
        {
            if (!isCompiled)
            {
                if (!File.Exists(sourceConfigFilePath))
                    { ExceptionsManager.CantFindSourceConfigFile(sourceConfigFilePath); }
                if (!File.Exists(loggerExecutableFilePath))
                    { ExceptionsManager.CantFindLoggerExecutableFile(loggerExecutableFilePath); }
                if (!Directory.Exists(contentFolderPath))
                    { ExceptionsManager.CantFindContentFolder(contentFolderPath); }
            }
        }
    }
}
