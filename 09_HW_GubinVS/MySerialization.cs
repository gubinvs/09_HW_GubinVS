using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace _09_HW_GubinVS
{
    class MySerialization
    {
        /// <summary>
        /// Json сериализация
        /// </summary>
        public static void JsonSerialize(string path, List<Document> docs)
        {
            string json = JsonConvert.SerializeObject(docs);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Метод десиарилазации данных о компании json из файла
        /// </summary>
        public static List<Document> JsonDeserializer(string fileJson)
        {
            string json = File.ReadAllText(fileJson);
            List<Document> Docs = JsonConvert.DeserializeObject<List<Document>>(json);
            return Docs;
        }


    }
}
