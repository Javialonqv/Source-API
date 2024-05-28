﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace Compiler
{
    public class Executable
    {
        public CompilerConfigFile configFile;

        /// <summary>
        /// Serializes the Config File into a Source Executable File.
        /// </summary>
        /// <param name="configFile">The needed ConfigFile instance.</param>
        /// <param name="srcFilePath">The path where the .src file will be created.</param>
        public static void CreateSRCFile(CompilerConfigFile configFile, string srcFilePath)
        {
            FileStream fs = new FileStream(srcFilePath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            Dictionary<string, object> toSerialize = new Dictionary<string, object>();
            /*toSerialize.Add("loggerEnabled", configFile.loggerEnabled);
            toSerialize.Add("mainAppProjName", configFile.mainAppProjName);
            toSerialize.Add("mainMethodClassName", configFile.mainMethodClassName);
            toSerialize.Add("packageName", configFile.packageName);*/
            Type type = configFile.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite && !property.GetMethod.IsStatic)
                {
                    toSerialize.Add(property.Name, property.GetValue(configFile));
                }
            }
            bf.Serialize(fs, toSerialize);
        }
    }
}
