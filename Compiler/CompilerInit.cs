using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    internal class CompilerInit
    {
        static void Main(string[] args)
        {
            CompilerPaths.Init();
            //DLLCompiler.Build(CompilerPaths.apiCsprojPath, CompilerPaths.tempDataPath, true, true);

            Console.WriteLine("[*] Compiling App Project...");
            DLLCompiler.Build(CompilerPaths.appCsprojPath, CompilerPaths.tempDataPath, true, true);
            Console.WriteLine("[*] Compiling Logger...");
            DLLCompiler.Build(CompilerPaths.loggerCsprojPath, CompilerPaths.tempDataPath, true, false);
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
    }
}
