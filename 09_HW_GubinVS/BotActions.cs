using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace _09_HW_GubinVS
{
    class BotActions
    {
        /// Класс с действиями бота



        /// <summary>
        /// Метод печатает в консоль принятое сообщение
        /// </summary>

        public static void PrintMessage(string userMessage, string userId, string useFirstrName)
        {
            string text = $"{useFirstrName} {userId} {userMessage}";

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
                //Console.WriteLine($"Здравствуйте, {useFirstrName}");
            }

        }




    }
}
