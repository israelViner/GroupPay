using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroupPay
{
    internal static class JsonFileUtils<T>
    {
        private static readonly JsonSerializerSettings Options = new() { NullValueHandling = NullValueHandling.Ignore };

        public static T? ReadJson(string json_file_path)
        {
            if (!File.Exists(json_file_path))
            {
                File.Create(json_file_path).Close();
            }

            var json = File.ReadAllText(json_file_path);
            T? users = JsonConvert.DeserializeObject<T>(json);
            return users;
        }

        public static void WriteJson(string json_file_path, T list_to_write)
        {
            var jsonString = JsonConvert.SerializeObject(list_to_write, Options);
            File.WriteAllText(json_file_path, jsonString);
        }
    }
}
