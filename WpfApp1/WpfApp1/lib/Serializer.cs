using Newtonsoft.Json;
using System;
using System.IO;

namespace WpfApp1.lib
{
    class Serializer
    {
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T Deserialize<T>(string serializedString)
        {
            T obj = JsonConvert.DeserializeObject<T>(serializedString);
            return obj;
        }
        public static void SaveToFile(string serializedString, string filename)
        {
            var writer = new StreamWriter(filename);
            writer.Write(serializedString);
            writer.Close();
        }
        public static string ReadFromFile(string filename)
        {
            try
            {
                var reader = new StreamReader(filename);
                string data = reader.ReadToEnd();
                reader.Close();
                return data;
            }
            catch (Exception err)
            {
                throw new Exception($"File not found: {err.Message}");
            }
        }
    }
}
