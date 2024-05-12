using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public static class Resources
    {
        static Dictionary<string, string> files = new Dictionary<string, string>();

        internal static void Init(bool isCompiled)
        {
            if (!isCompiled)
            {
                List<string> folderContents = Directory.GetFiles(Paths.contentFolderPath, "*.*",
                    SearchOption.AllDirectories).ToList();
                foreach (string filePath in folderContents)
                {
                    string key = filePath.Substring(filePath.IndexOf("Content") + "Content".Length + 1);
                    files.Add(key.Replace('\\', '/'), filePath);
                }
            }
        }

        public static T Load<T>(string fileName)
        {
            foreach (var pair in files)
            {
                if (pair.Key == fileName || Path.GetFileName(pair.Key) == fileName ||
                    Path.GetFileNameWithoutExtension(pair.Key) == fileName)
                {
                    if (typeof(T) == typeof(string))
                    {
                        return (T)(object)File.ReadAllText(pair.Value);
                    }
                    else { return (T)(object)File.ReadAllBytes(pair.Value); }
                }
            }
            ExceptionsManager.CantFindTheSpecifiedResource(fileName);
            return default(T);
        }

        public static object Load(string fileName)
        {
            foreach (var pair in files)
            {
                if (pair.Key == fileName || Path.GetFileName(pair.Key) == fileName ||
                    Path.GetFileNameWithoutExtension(pair.Key) == fileName)
                {
                    return File.ReadAllBytes(pair.Value);
                }
            }
            ExceptionsManager.CantFindTheSpecifiedResource(fileName);
            return null;
        }
    }
}
