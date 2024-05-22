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
            CompilerPaths.Init();

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

        static T ReadJSONProperty<T>(string jsonFilePath, string propertyName)
        {
            string input = File.ReadAllText(jsonFilePath);
            JObject jsonObj = JObject.Parse(input);
            return jsonObj[propertyName].Value<T>();
        }

        static void BuildAllCsprojs()
        {
            foreach (string csprojPath in Directory.GetFiles(CompilerPaths.solutionPath, "*.csproj", SearchOption.AllDirectories))
            {
                if (Path.GetFileName(csprojPath) == "Compiler.csproj") { continue; }
                Console.WriteLine($"[*] Compiling {Path.GetFileName(csprojPath)}...");

                bool showCompilerLog = ReadJSONProperty<bool>(CompilerPaths.compilerPrefJsonPath, "showCompilerLog");
                if (Path.GetFileName(csprojPath) == "Logger.csproj")
                { DLLCompiler.BuildAsExe(csprojPath, CompilerPaths.tempDataPath, showCompilerLog); }
                else
                { DLLCompiler.BuildAsDLL(csprojPath, CompilerPaths.tempDataPath, showCompilerLog); }
            }
        }
    }
}
