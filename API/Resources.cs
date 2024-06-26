﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace API
{
    /// <summary>
    /// Toolkit for loading resources files inside of the "Content" folder.
    /// </summary>
    public static class Resources
    {
        /// <summary>
        /// Contains all the files inside of the "Content" folder and subfolders.
        /// It have the path to the file (with "Content" as root) as key and the full file path as value.
        /// </summary>
        static Dictionary<string, string> files = new Dictionary<string, string>();

        /// <summary>
        /// Inits the Resources class and files to be used during runtime.
        /// </summary>
        /// <param name="isCompiled">Specifies if the App is compiled with the Source Compiler.</param>
        internal static void Init(bool isCompiled)
        {
            if (!isCompiled) // If NOT compiled with the Source compiler:
            {
                List<string> folderContents = Directory.GetFiles(Paths.contentFolderPath, "*.*",
                    SearchOption.AllDirectories).ToList(); // Get all the files inside content folder.
                foreach (string filePath in folderContents)
                {
                    // Get only the string after the "Content" folder as key:
                    string key = filePath.Substring(filePath.IndexOf("Content") + "Content".Length + 1);
                    // Add the key and full file path into the files list:
                    files.Add(key.Replace('\\', '/'), filePath);
                }
            }
            else // If compiled with the Source Compiler:
            {
                // Deserealize the resources.data file.
                FileStream fs = new FileStream(Paths.resourcesDataFilePath, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                Dictionary<string, string> resources = (Dictionary<string, string>)bf.Deserialize(fs);
                // Foreach pair in the resources file, add the relative path as key and the full file path as value.
                foreach (var pair in resources)
                {
                    string filePath = Path.Combine(Paths.contentFolderPath, pair.Value);
                    files.Add(pair.Key, filePath);
                }
            }
        }

        /// <summary>
        /// Loads a specified file from the "Content" folder if exists.
        /// </summary>
        /// <typeparam name="T">The type of the value to return.</typeparam>
        /// <param name="fileName">The name of the file, can be with/without extension, or a file path relative to the
        /// "Content" folder.</param>
        /// <returns>The specified file converted to the specified type.</returns>
        public static T Load<T>(string fileName)
        {
            foreach (var pair in files)
            {
                // If the fileName equals to the key, or the name with/without extension:
                if (pair.Key == fileName || Path.GetFileName(pair.Key) == fileName ||
                    Path.GetFileNameWithoutExtension(pair.Key) == fileName)
                {
                    if (typeof(T) == typeof(string))
                    {
                        return (T)(object)File.ReadAllText(pair.Value);
                    }
                    else if (typeof(T) == typeof(AudioSource) && AudioSource.CanCreateFrom(pair.Value))
                    {
                        return (T)(object)(new AudioSource(pair.Value));
                    }
                    else { return (T)(object)File.ReadAllBytes(pair.Value); }
                }
            }
            ExceptionsManager.CantFindTheSpecifiedResource(fileName);
            return default(T);
        }

        /// <summary>
        /// Loads a specified file from the "Content" folder if exists.
        /// </summary>
        /// <param name="fileName">The name of the file, can be with/without extension, or a file path relative to the
        /// "Content" folder.</param>
        /// <returns>The specified file to an object type.</returns>
        public static object Load(string fileName)
        {
            foreach (var pair in files)
            {
                // If the fileName equals to the key, or the name with/without extension:
                if (pair.Key == fileName || Path.GetFileName(pair.Key) == fileName ||
                    Path.GetFileNameWithoutExtension(pair.Key) == fileName)
                {
                    return File.ReadAllBytes(pair.Value);
                }
            }
            ExceptionsManager.CantFindTheSpecifiedResource(fileName);
            return null;
        }

        /// <summary>
        /// Gets the full file path of the specified resource file.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>The full file path.</returns>
        public static string GetFullFilePath(string fileName)
        {
            foreach (var pair in files)
            {
                // If the fileName equals to the key, or the name with/without extension:
                if (pair.Key == fileName || Path.GetFileName(pair.Key) == fileName ||
                    Path.GetFileNameWithoutExtension(pair.Key) == fileName)
                {
                    return pair.Value;
                }
            }
            ExceptionsManager.CantFindTheSpecifiedResource(fileName);
            return "";
        }
    }
}
