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
        /// Points to the SourceConfig.json file. This one only exists on debug mode.
        /// </summary>
        public static string sourceConfigFilePath = "";
        /// <summary>
        /// Points to the Logger.exe file.
        /// </summary>
        public static string loggerExecutableFilePath = "";
        /// <summary>
        /// Points to the "Content" folder.
        /// </summary>
        public static string contentFolderPath = "";

        /// <summary>
        /// Inits the paths to be used during runtime.
        /// </summary>
        /// <param name="isCompiled">Defines if the current app is already compiled.</param>
        internal static void Init(bool isCompiled)
        {
            if (!isCompiled)
            {
                /*sourceConfigFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                    "App", "SourceConfig.json");*/
                sourceConfigFilePath = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                    "SourceConfig.json", SearchOption.AllDirectories)[0];
                loggerExecutableFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logger.exe");
                contentFolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                    "App", "Content");
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
