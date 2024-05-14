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
            DLLCompiler.BuildDLL(CompilerPaths.apiCsprojPath, CompilerPaths.tempDataPath, true, true);
            DLLCompiler.BuildDLL(CompilerPaths.appCsprojPath, CompilerPaths.tempDataPath, true, true);
            DLLCompiler.BuildDLL(CompilerPaths.loggerCsprojPath, CompilerPaths.tempDataPath, true, false);
            DLLCompiler.CopyDLLsToDataFolder(CompilerPaths.tempDataPath, CompilerPaths.dataPath);
            Console.ReadKey();
        }
    }
}
