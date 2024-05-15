using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using System.Diagnostics;
using System.IO;

namespace Compiler
{
    internal class DLLCompiler
    {
        public static void Build(string csprojPath, string outputPath, bool showLog, bool buildAsLibrary)
        {
            string argument = "";
            if (buildAsLibrary) { argument = $"build \"{csprojPath}\" -o \"{outputPath}\" -c Release /p:DebugType=none /p:OutputType=Library"; }
            else { argument = $"build \"{csprojPath}\" -o \"{outputPath}\" -c Release /p:DebugType=none /p:OutputType=Exe"; }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = argument,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                if (showLog) { process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data); }
                process.Start();
                if (showLog) { process.BeginOutputReadLine(); }
                process.WaitForExit();
            }

            /*var globalProperties = new Dictionary<string, string>
            {
                { "OutputPath", outputPath },
                { "Configuration", "Release" },
                { "DebugType", "none" },
                { "OutputType", "Library" },
            };
            var buildParameters = new BuildParameters();
            if (showLog) { buildParameters.Loggers = new[] { new ConsoleLogger() }; }

            var buildRequestData = new BuildRequestData(csprojPath, globalProperties, "4.0", new[] { "Build" }, null);
            var buildResult = BuildManager.DefaultBuildManager.Build(buildParameters, buildRequestData);*/
        }

        public static void CopyDLLsToDataFolder(string tempPath, string dataPath)
        {
            List<string> tempPathFiles = Directory.GetFiles(tempPath, "*.dll").ToList();
            tempPathFiles.AddRange(Directory.GetFiles(tempPath, "*.exe"));
            foreach (string file in tempPathFiles)
            {
                string destFile = Path.Combine(dataPath, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }
            Directory.Delete(tempPath, true);
        }

        public static void BuildContentFolder(string projContentPath, string contentPath)
        {
            Directory.CreateDirectory(contentPath);
            foreach (string file in Directory.GetFiles(projContentPath, "*.*", SearchOption.AllDirectories))
            {
                string newFilePath = Path.Combine(contentPath, file.Substring(projContentPath.Length + 1));
                if (!Directory.Exists(Path.GetDirectoryName(newFilePath)))
                    { Directory.CreateDirectory(Path.GetDirectoryName(newFilePath)); }
                File.Copy(file, newFilePath, true);
            }
        }
    }
}
