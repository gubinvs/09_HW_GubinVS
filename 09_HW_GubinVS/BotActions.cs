using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace _09_HW_GubinVS
{
    class BotActions
    {
        /// Класс с действиями бота



        /// <summary>
        /// Метод печатает в консоль принятое сообщение
        /// </summary>

        public static void PrintMessage(string userMessage, string useFirstrName)
        {
            string text = $"{useFirstrName} {userMessage}";

            Console.WriteLine(text);

        }


        /// <summary>
        /// Метод отправляет сообщение пользователю на знакомый вопрос
        /// </summary>

        public static void SendMessage(WebClient wc, string startUrl, string userMessage, string userId, string useFirstrName)
        {
            if (userMessage == "hi" || userMessage == "Привет") // нужно сделать проверку из массива, если это приветствие
            {
                string responseText = $"Здравствуйте, {useFirstrName}";
                string url = $"{startUrl}sendMessage?chat_id={userId}&text={responseText}";
                wc.DownloadString(url);
                Console.WriteLine($"Здравствуйте, {useFirstrName}");
            }

        }

        /// <summary>
        /// Метод принимает десериализованный json (с сообщением в котором есть информация о файле)
        /// и скачивает файл в папку
        /// </summary>
       
        public static void DownloadFile(GetUpdates getUpdates)
        {

            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };
            string file_id = getUpdates.result[0].message.document.file_id;
            string file_name = getUpdates.result[0].message.document.file_name;

            // Запрос на сервер telegrfm для получения ссылки на файл в формате json
            var w = wc.DownloadString(Config.GetFile + file_id);

            // Заполнение структуры из сообщения json
            GetFile gf = JsonSerializer.Deserialize<GetFile>(w);
            //  Запрос на сервер telegram для скачивания файла
            wc.DownloadFile(Config.DownloadFile + gf.result.file_path, Config.PathDownloadFile + $"{file_name}");

        }

        public static void DownloadFoto(GetUpdates getUpdates)
        {
            Console.WriteLine("Пришло сообщение содержащее фото");
        
        }


    }
}
