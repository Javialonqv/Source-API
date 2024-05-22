using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    internal static class CompilerPaths
    {
        /// <summary>
        /// Points to the solution path.
        /// </summary>
        public static string solutionPath = "";
        /// <summary>
        /// Points to the API.csproj file.
        /// </summary>
        public static string apiCsprojPath = "";
        /// <summary>
        /// Points to the SourceConfig.json file inside of the Main App Project.
        /// </summary>
        public static string sourceConfigFilePath = "";
        /// <summary>
        /// Points to the Content folder inside of the Main App Project.
        /// </summary>
        public static string projContentPath = "";
        /// <summary>
        /// Points to the Logger.csproj file.
        /// </summary>
        public static string loggerCsprojPath = "";
        /// <summary>
        /// Points to the preferences.json file inside of the Compiler project (this one).
        /// </summary>
        public static string compilerPrefJsonPath = "";

        /// <summary>
        /// Points to the ReleaseBuild folder where the build will occur.
        /// </summary>
        public static string buildPath = "";
        /// <summary>
        /// Points to the data folder inside of the build path.
        /// </summary>
        public static string dataPath = "";
        /// <summary>
        /// Points to the temp folder inside of the data path.
        /// </summary>
        public static string tempDataPath = "";
        /// <summary>
        /// Points to the Content folder inside of the build path.
        /// </summary>
        public static string contentPath = "";
        /// <summary>
        /// Points to the default.src file path.
        /// </summary>
        public static string srcFilePath = "";
        /// <summary>
        /// Points to the resources.data file path.
        /// </summary>
        public static string resourcesDataFilePath = "";
        /// <summary>
        /// Points to the compiler.log file inside of the Compiler builded path.
        /// </summary>
        public static string logFilePath = "";

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
            logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "compiler.log");
        }
    }
}
