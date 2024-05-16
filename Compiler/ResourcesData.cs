using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Compiler
{
    [Serializable]
    internal class ResourcesData
    {
        /// <summary>
        /// Contains all the files inside of the builded "Content" folder and subfolders.
        /// It have the path to the file (with "Content" as root) as key and the file name as value.
        /// </summary>
        Dictionary<string, string> resources = new Dictionary<string, string>();

        public static void CreateResourcesData(Dictionary<string, string> resources, string resourcesDataPath)
        {
            ResourcesData data = new ResourcesData();
            data.resources = resources;

            FileStream fs = new FileStream(resourcesDataPath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, data);
        }

        public static string GenerateRandomName()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            int length = random.Next(10, 12);
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
