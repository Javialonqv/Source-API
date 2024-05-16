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
        public static string solutionPath = "";
        public static string apiCsprojPath = "";
        public static string sourceConfigFilePath = "";
        public static string projContentPath = "";
        public static string loggerCsprojPath = "";
        public static string compilerPrefJsonPath = "";

        public static string buildPath = "";
        public static string dataPath = "";
        public static string tempDataPath = "";
        public static string contentPath = "";
        public static string srcFilePath = "";
        public static string resourcesDataFilePath = "";

        public static void Init()
        {
            solutionPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            apiCsprojPath = Path.Combine(solutionPath, "API", "API.csproj");
            sourceConfigFilePath = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                "SourceConfig.json", SearchOption.AllDirectories)[0];
            projContentPath = Path.Combine(Path.GetDirectoryName(sourceConfigFilePath), "Content");
            loggerCsprojPath = Path.Combine(solutionPath, "Logger", "Logger.csproj");
            compilerPrefJsonPath = Path.Combine(solutionPath, "Compiler", "preferences.json");

            buildPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                "Builds", "ReleaseBuild");
            dataPath = Path.Combine(buildPath, "data");
            tempDataPath = Path.Combine(dataPath, "temp");
            contentPath = Path.Combine(buildPath, "Content");
            srcFilePath = Path.Combine(buildPath, "default.src");
            resourcesDataFilePath = Path.Combine(contentPath, "resources.data");
        }
    }
}
