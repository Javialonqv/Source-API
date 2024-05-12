using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace API
{
    internal static class Paths
    {
        public static string sourceConfigFilePath = "";
        public static string loggerExecutableFilePath = "";
        public static string contentFolderPath = "";

        internal static void Init(bool isCompiled)
        {
            if (!isCompiled)
            {
                sourceConfigFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                    "App", "SourceConfig.json");
                loggerExecutableFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logger.exe");
                contentFolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                    "App", "Content");
            }
            CheckDirectories(isCompiled);
        }

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
