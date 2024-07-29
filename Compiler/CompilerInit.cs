using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Compiler
{
    internal class CompilerInit
    {
        static void Main(string[] args)
        {
            // Init the paths and delete the log file if it exists.
            CompilerPaths.Init();
            if (!Directory.Exists(CompilerPaths.buildPath)) { Directory.CreateDirectory(CompilerPaths.buildPath); }
            DLLCompiler.DeleteLogFile();

            // Delte the previous build ONLY if that property is true in the preferences.json file.
            if (ReadJSONProperty<bool>(CompilerPaths.compilerPrefJsonPath, "deletePreviousBuild"))
            {
                Console.WriteLine("[*] Deleting previous build...");
                DeletePreviousBuild();
            }
            Console.WriteLine("[*] Compiling .csproj projects...");
            BuildAllCsprojs();
            Console.WriteLine("[*] Saving needed files...");
            DLLCompiler.CopyDLLsToDataFolder(CompilerPaths.tempDataPath, CompilerPaths.dataPath);
            Console.WriteLine("[*] Buiding Content folder...");
            DLLCompiler.BuildContentFolder(CompilerPaths.projContentPath, CompilerPaths.contentPath);
            Console.WriteLine("[*] Creating SRC file...");
            CompilerConfigFile.Init(CompilerPaths.sourceConfigFilePath);
            Executable.CreateSRCFile(CompilerConfigFile.loaded, CompilerPaths.srcFilePath);
            Console.WriteLine("[*] FINISH!!!");
            Console.ReadKey();
        }

        static void DeletePreviousBuild()
        {
            foreach (string dir in Directory.GetDirectories(CompilerPaths.buildPath))
            {
                Directory.Delete(dir, true);
            }
            foreach (string dir in Directory.GetFiles(CompilerPaths.buildPath))
            {
                File.Delete(dir);
            }
        }

        public static T ReadJSONProperty<T>(string jsonFilePath, string propertyName)
        {
            string input = File.ReadAllText(jsonFilePath);
            JObject jsonObj = JObject.Parse(input);
            return jsonObj[propertyName].Value<T>();
        }

        static void BuildAllCsprojs()
        {
            // This looks for all the .csproj in the project, except the Compiler, the Logger is compiled as Exe, and the rest as DLL.
            foreach (string csprojPath in Directory.GetFiles(CompilerPaths.solutionPath, "*.csproj", SearchOption.AllDirectories))
            {
                if (Path.GetFileName(csprojPath) == "Compiler.csproj") { continue; }
                Console.Write($"[*] Compiling {Path.GetFileName(csprojPath)}...");

                bool showCompilerLog = ReadJSONProperty<bool>(CompilerPaths.compilerPrefJsonPath, "showCompilerLog");
                bool compilationSuccess = false;
                bool saveLog = ReadJSONProperty<bool>(CompilerPaths.compilerPrefJsonPath, "saveLogFile");
                if (Path.GetFileName(csprojPath) == "Logger.csproj")
                { compilationSuccess = DLLCompiler.BuildAsExe(csprojPath, CompilerPaths.tempDataPath, showCompilerLog, saveLog); }
                else
                { compilationSuccess = DLLCompiler.BuildAsDLL(csprojPath, CompilerPaths.tempDataPath, showCompilerLog, saveLog); }

                if (compilationSuccess && !showCompilerLog)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Done");
                }
                else if (!showCompilerLog)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (saveLog) { Console.Write("FAILED!!!"); }
                    else { Console.WriteLine("FAILED!!!"); }
                    Console.ForegroundColor = ConsoleColor.White;
                    if (saveLog) { Console.WriteLine(" See compiler.log for more details."); }
                }
            }
        }
    }
}
