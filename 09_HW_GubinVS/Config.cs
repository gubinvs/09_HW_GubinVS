using System.IO;
using System.Text;

namespace _09_HW_GubinVS
{
    class Config
    {

        /// <summary>
        ///  Путь к файлу с токеном телеграм бота
        /// </summary>
        private static string path = @"C:\teltoken.txt";

        /// <summary>
        /// Токен чат бота телеграмм, считывается из файла
        /// </summary>
        public static string Token { get;} = File.ReadAllText(path, Encoding.UTF8);

        /// <summary>
        /// Ссылка-запрос на сервер о наличии новых сообщений боту
        /// </summary>
        public static string GetUpdates { get; } = $@"https://api.telegram.org/bot{Token}/getUpdates?offset="; // добавить обязательный параметр update_id

        /// <summary>
        /// Ссылка-запрос на сервер для получения параметра file_path 
        /// данный параметр поступает в ответе сервера и необходим для скачивания файла отправленного пользователем боту
        /// </summary>
        public static string GetFile { get;} = $@"https://api.telegram.org/bot{Token}/getFile?file_id="; // добавить обязательный параметр file_id

        /// <summary>
        /// Ссылка-запрос на сервер для скачивания полученного файла
        /// </summary>
        public static string DownloadFile { get; } = $@"https://api.telegram.org/file/bot{Token}/"; // добавить обязательный параметр file_path

    }
}
