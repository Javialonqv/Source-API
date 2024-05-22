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
        public static bool BuildAsDLL(string csprojPath, string outputPath, bool showLog, bool saveLog)
        {
            string argument = $"build \"{csprojPath}\" -o \"{outputPath}\" -c Release /p:DebugType=none /p:OutputType=Library";

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
                if (saveLog) { process.OutputDataReceived += (sender, e) => WriteOnLogFile(e.Data); }
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
                return process.ExitCode == 0;
            }
        }
        public static bool BuildAsExe(string csprojPath, string outputPath, bool showLog, bool saveLog)
        {
            string argument = $"build \"{csprojPath}\" -o \"{outputPath}\" -c Release /p:DebugType=none /p:OutputType=Exe";

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
                if (saveLog) { process.OutputDataReceived += (sender, e) => WriteOnLogFile(e.Data); }
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
                return process.ExitCode == 0;
            }
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
            Dictionary<string, string> resources = new Dictionary<string, string>();
            foreach (string file in Directory.GetFiles(projContentPath, "*.*", SearchOption.AllDirectories))
            {
                string randomName = ResourcesData.GenerateRandomName();
                string newFilePath = Path.Combine(contentPath, randomName);
                if (!Directory.Exists(Path.GetDirectoryName(newFilePath)))
                    { Directory.CreateDirectory(Path.GetDirectoryName(newFilePath)); }
                File.Copy(file, newFilePath, true);

                // Get only the string after the "Content" folder as key:
                string key = file.Substring(file.IndexOf("Content") + "Content".Length + 1);
                // Add the key and new file path into the resources list:
                string newFileName = randomName;
                resources.Add(key.Replace('\\', '/'), newFileName);
            }
            ResourcesData.CreateResourcesData(resources, CompilerPaths.resourcesDataFilePath);
        }

        public static void DeleteLogFile()
        {
            if (File.Exists(CompilerPaths.logFilePath)) { File.Delete(CompilerPaths.logFilePath); }
        }
        static void WriteOnLogFile(string toWrite)
        {
            List<string> logFileContent = new List<string>();
            if (File.Exists(CompilerPaths.logFilePath)) { logFileContent = File.ReadAllLines(CompilerPaths.logFilePath).ToList(); }
            logFileContent.Add(toWrite);
            File.WriteAllLines(CompilerPaths.logFilePath, logFileContent);
        }
    }
}
