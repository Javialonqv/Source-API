using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    internal static class CompilerPaths
    {
        public static string apiCsprojPath = "";
        public static string appCsprojPath = "";
        public static string loggerCsprojPath = "";
        public static string buildPath = "";
        public static string dataPath = "";
        public static string tempDataPath = "";
        public static string contentPath = "";
        public static string srcFilePath = "";

        public static void Init()
        {
            apiCsprojPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                "API", "API.csproj");
            appCsprojPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                "App", "App.csproj");
            loggerCsprojPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                "Logger", "Logger.csproj");
            buildPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                "Builds", "ReleaseBuild");
            dataPath = Path.Combine(buildPath, "data");
            tempDataPath = Path.Combine(dataPath, "temp");
            contentPath = Path.Combine(buildPath, "Content");
            srcFilePath = Path.Combine(buildPath, "default.src");
        }
    }
}
