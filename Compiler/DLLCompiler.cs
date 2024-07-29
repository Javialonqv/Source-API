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
        /// <summary>
        /// Builds a .csproj file as DLL.
        /// </summary>
        /// <param name="csprojPath">The path to the .csproj file.</param>
        /// <param name="outputPath">The path to the output build path.</param>
        /// <param name="showLog">Specifies if should show the log into the console.</param>
        /// <param name="saveLog">Specifies if needs to save a log file.</param>
        /// <returns>Returns if the build has been successfully.</returns>
        public static bool BuildAsDLL(string csprojPath, string outputPath, bool showLog, bool saveLog)
        {
            string argument = $"publish \"{csprojPath}\" -o \"{outputPath}\" -c Release /p:DebugType=none /p:OutputType=Library /p:DefineConstants=RELEASE_BUILD";

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
        /// <summary>
        /// Builds a .csproj file as an executable.
        /// </summary>
        /// <param name="csprojPath">The path to the .csproj file.</param>
        /// <param name="outputPath">The path to the output build path.</param>
        /// <param name="showLog">Specifies if should show the log into the console.</param>
        /// <param name="saveLog">Specifies if needs to save a log file.</param>
        /// <returns>Returns if the build has been successfully.</returns>
        public static bool BuildAsExe(string csprojPath, string outputPath, bool showLog, bool saveLog)
        {
            string argument = $"publish \"{csprojPath}\" -o \"{outputPath}\" -c Release /p:DebugType=none /p:OutputType=Exe /p:DefineConstants=RELEASE_BUILD /p:PublishSingleFile=true";

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

        /// <summary>
        /// Copy only the needed dlls to the data folder.
        /// </summary>
        /// <param name="tempPath">The path to the temp folder.</param>
        /// <param name="dataPath">The path to the data folder.</param>
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

        /// <summary>
        /// Builds the content folder.
        /// </summary>
        /// <param name="projContentPath">The Content folder inside of the project.</param>
        /// <param name="contentPath">The path to the Content builded folder.</param>
        public static void BuildContentFolder(string projContentPath, string contentPath)
        {
            Directory.CreateDirectory(contentPath);
            List<string> usedNames = new List<string>();
            Dictionary<string, string> resources = new Dictionary<string, string>();
            foreach (string file in Directory.GetFiles(projContentPath, "*.*", SearchOption.AllDirectories))
            {
                string randomName = ResourcesData.GenerateRandomName();
                // Generate new names until a new one appears:
                while (usedNames.Contains(randomName)) { randomName = ResourcesData.GenerateRandomName(); }
                string newFilePath = Path.Combine(contentPath, randomName);
                if (!Directory.Exists(Path.GetDirectoryName(newFilePath)))
                    { Directory.CreateDirectory(Path.GetDirectoryName(newFilePath)); }
                File.Copy(file, newFilePath, true);

                // Get only the string after the "Content" folder as key:
                string key = file.Substring(file.IndexOf("Content") + "Content".Length + 1);
                // Add the key and new file path into the resources list:
                string newFileName = randomName;
                resources.Add(key.Replace('\\', '/'), newFileName);
                usedNames.Add(randomName);
            }
            ResourcesData.CreateResourcesData(resources, CompilerPaths.resourcesDataFilePath);
        }

        /// <summary>
        /// Deletes the log file.
        /// </summary>
        public static void DeleteLogFile()
        {
            if (File.Exists(CompilerPaths.logFilePath)) { File.Delete(CompilerPaths.logFilePath); }
        }
        /// <summary>
        /// Writes a string into the log file.
        /// </summary>
        /// <param name="toWrite"></param>
        static void WriteOnLogFile(string toWrite)
        {
            List<string> logFileContent = new List<string>();
            if (File.Exists(CompilerPaths.logFilePath)) { logFileContent = File.ReadAllLines(CompilerPaths.logFilePath).ToList(); }
            logFileContent.Add(toWrite);
            File.WriteAllLines(CompilerPaths.logFilePath, logFileContent);
        }
    }
}
