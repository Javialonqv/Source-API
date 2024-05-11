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

        internal static void Init(bool isCompiled)
        {
            if (!isCompiled)
            {
                sourceConfigFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName,
                    "App", "SourceConfig.json");
                loggerExecutableFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName,
                    "API", "Logger.exe");
            }
        }
    }
}
