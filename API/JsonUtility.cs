using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API
{
    /// <summary>
    /// Toolkit for managing JSON texts.
    /// </summary>
    public static class JsonUtility
    {
        /// <summary>
        /// Converts the public properties from a .NET object to a JSON string.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <param name="prettyPrint">Specifies if the JSON object should have indentation.</param>
        /// <returns>The .NET object converted to a JSON text.</returns>
        public static string ToJson(object obj, bool prettyPrint = true)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error,
                Formatting = prettyPrint ? Formatting.Indented : Formatting.None,
            };
            try
            {
                string json = JsonConvert.SerializeObject(obj, settings);
                return json;
            }
            catch
            {
                ExceptionsManager.CantSerializeJsonFile(obj.GetType());
            }
            return "";
        }

        /// <summary>
        /// Converts a JSON text to a .NET object.
        /// </summary>
        /// <typeparam name="T">The object to convert to.</typeparam>
        /// <param name="json">The JSON text.</param>
        /// <returns>The JSON text converted to a .NET object.</returns>
        public static T FromJson<T>(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                MissingMemberHandling= MissingMemberHandling.Error
            };
            try
            {
                T obj = JsonConvert.DeserializeObject<T>(json, settings);
                return obj;
            }
            catch
            {
                ExceptionsManager.CantDeserializeJsonFile(typeof(T));
            }
            return default(T);
        }
        /// <summary>
        /// Converts a JSON text and set it to the specified .NET object.
        /// </summary>
        /// <param name="json">The JSON text.</param>
        /// <param name="obj">Reference to the obj to set the properties.</param>
        public static void FromJson(string json, ref object obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };
            try
            {
                obj = JsonConvert.DeserializeObject<object>(json, settings);
            }
            catch
            {
                ExceptionsManager.CantDeserializeJsonFile(typeof(object));
            }
        }

        /// <summary>
        /// Deserialize the specified property from the specified json text.
        /// </summary>
        /// <typeparam name="T">The object to convert to.</typeparam>
        /// <param name="json">The JSON text.</param>
        /// <param name="propertyName">The name of the property to deserialize.</param>
        /// <returns>The deserialized property.</returns>
        static T ReadJSONProperty<T>(string json, string propertyName)
        {
            JObject jsonObj = JObject.Parse(json);
            if (jsonObj.ContainsKey(propertyName))
            {
                return jsonObj[propertyName].Value<T>();
            }
            else
            {
                ExceptionsManager.CantDeserializeJsonFile(typeof(T));
                return default(T);
            }
        }
    }
}
