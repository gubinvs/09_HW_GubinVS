using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace _09_HW_GubinVS
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string token = Config.Token; // Токен телеграмм бота
            string startUrl = $@"https://api.telegram.org/bot{token}/"; // запрос боту
           

            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };
            int update_id = 0;

            while (true)
            {
                string url = $"{startUrl}getUpdates?offset={update_id}";
                var r = wc.DownloadString(url);

                var msgs = JObject.Parse(r)["result"].ToArray();

                foreach (dynamic msg in msgs)
                {
                    update_id = Convert.ToInt32(msg.update_id) + 1;

                    string userMessage = msg.message.text;
                    string userId = msg.message.from.id;
                    string useFirstrName = msg.message.from.first_name;

                    string text = $"{useFirstrName} {userId} {userMessage}";
                   // Console.WriteLine(r);
                    Console.WriteLine(text);

                    if (userMessage == "hi")
                    {
                        string responseText = $"Здравствуйте, {useFirstrName}";
                        url = $"{startUrl}sendMessage?chat_id={userId}&text={responseText}";
                        
                        Console.WriteLine(wc.DownloadString(url));
                    }
                }


                Thread.Sleep(100);
            }

            



            Console.ReadKey();
        }
    }
}
