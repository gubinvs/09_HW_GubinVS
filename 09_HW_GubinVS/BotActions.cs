using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// Метод отправляет ответ пользователю на его сообщение
        /// </summary>

        public static void SendMessageText(WebClient wc, GetUpdates gu)
        {
            string startUrl = Config.StartUrl;                                                  //  Получение основного адреса сервера telegram
            int userId = gu.result[0].message.from.id;                                          //  Получение параметра идентификатора сообщения
            string userMessage = gu.result[0].message.text;                                     //  Полуение текста сообщения
            string userFirstName = gu.result[0].message.from.first_name;

            if (userMessage == "/start")                                                        // ответное сообщение на команду start
            {
                string text = $"Здравствуйте, {userFirstName}\n"+
                              "Я умею:\n"+
                              "1. Принимать Ваши файлы;\n" +
                              "2. Выводить список документов, которые можно скачать;\n" +
                              "3. Отвечаю на часто задаваемые вопросы";

                string message = $"{startUrl}sendMessage?chat_id={userId}&text={text}";
                wc.DownloadString(message);
            }
            else if(userMessage == "file")
            {
                string[] file = Directory.GetFiles(@"E:\");
                foreach (var item in file)
                {
                    Console.WriteLine(item);                
                    string message = $"{startUrl}sendMessage?chat_id={userId}&text={item}";
                    wc.DownloadString(message);
                }


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


        /// <summary>
        /// Метод принимает сообщение и скачивает фото
        /// </summary>
        
        public static void DownloadFoto(GetUpdates getUpdates)
        {
            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };

            string file_id = getUpdates.result[0].message.photo[2].file_id;         

            // Запрос на сервер telegrfm для получения ссылки на файл в формате json
            var w = wc.DownloadString(Config.GetFile + file_id);

            // Заполнение структуры из сообщения json
            GetFile gf = JsonSerializer.Deserialize<GetFile>(w);
            string file_name = gf.result.file_path.Remove(0, 7); // Дополнительно удалил от начала 7 символов
            
            //  Запрос на сервер telegram для скачивания файла
            wc.DownloadFile(Config.DownloadFile + gf.result.file_path, Config.PathDownloadFile + $"{file_name}");


        }

        /// <summary>
        /// Метод получения стикера (когда будет графический интерфейс)
        /// </summary>
        /// <param name="getUpdates"></param>
        public static void DownloadSticker(GetUpdates getUpdates)
        {

            Console.WriteLine("Пришел стикер, как сделаете графический интерфей - покажу )");
        }

        /// <summary>
        /// Метод получения звукового файла
        /// </summary>
        /// <param name="getUpdates"></param>
        public static void DownloadVoice(GetUpdates getUpdates)
        {

            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };

            string file_id = getUpdates.result[0].message.voice.file_id;

            // Запрос на сервер telegrfm для получения ссылки на файл в формате json
            var w = wc.DownloadString(Config.GetFile + file_id);
            Console.WriteLine(w);
            // Заполнение структуры из сообщения json
            GetFile gf = JsonSerializer.Deserialize<GetFile>(w);
            string file_name = gf.result.file_path.Remove(0, 7); // Дополнительно удалил от начала 7 символов

            //  Запрос на сервер telegram для скачивания файла
            wc.DownloadFile(Config.DownloadFile + gf.result.file_path, Config.PathDownloadFile + $"{file_name}");

        }

        /// <summary>
        /// Метод получения видео файла
        /// </summary>
        /// <param name="getUpdates"></param>
        public static void DownloadVideo(GetUpdates getUpdates)
        {

            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };

            string file_id = getUpdates.result[0].message.video_note.file_id;

            // Запрос на сервер telegrfm для получения ссылки на файл в формате json
            var w = wc.DownloadString(Config.GetFile + file_id);
            Console.WriteLine(w);
            // Заполнение структуры из сообщения json
            GetFile gf = JsonSerializer.Deserialize<GetFile>(w);
            string file_name = gf.result.file_path.Remove(0, 12); // Дополнительно удалил от начала 7 символов

            //  Запрос на сервер telegram для скачивания файла
            wc.DownloadFile(Config.DownloadFile + gf.result.file_path, Config.PathDownloadFile + $"{file_name}");

        }



    }
}
