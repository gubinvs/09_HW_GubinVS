using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace _09_HW_GubinVS
{
    // Создать бота, позволяющего принимать разные типы файлов, 
    // *Научить бота отправлять выбранный файл в ответ


    class Program
    {
        static void Main(string[] args)
        {
            
            string token = Config.Token;                                                                            // Уникальний идентификатор (Токен) бота считанный из файла с помощью класса Config
            string startUrl = $@"https://api.telegram.org/bot{token}/";                                             // адрес сервера telegram с параметрами бота которому он адресован
           
            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };                                            // экземпляр класса работы с сетью
            
            int update_id = 0;                                                                                      // возвращаемый номер сообщения обработанного методом по умолчанию = 0
            
           
            while (true)                                                                                            // Цикл бесконечно отправляет запросы серверу Telegram
            {
                string url = $"{startUrl}getUpdates?offset={update_id}";
                var str = wc.DownloadString(url);                                                                   // объявление переменной в которую помещается ответ полученный с сервера по запросу = getUpdates
                var msgs = JObject.Parse(str)["result"].ToArray();                                                  // Объявляется переменная в  которую парсится полученный из запроса ответ в виде json файла



                foreach (dynamic msg in msgs)                                                                       // цикл перебирает масссив созданный из полученного файла json при получение
                {
                   
                    update_id = Convert.ToInt32(msg.update_id) + 1;

                    string userMessage = msg.message.text;
                    string userId = msg.message.from.id;
                    string useFirstrName = msg.message.from.first_name;

                    

                    if (CheckingInputParameters.ChekDocument(msg))                                                  //  Проверка на наличие полей document
                    {
                        
                        
                    }
                    
                    
                }

                








                Thread.Sleep(100);                                                                                   //  Остановка потока на 100 мс
            }

            



            Console.ReadKey();
        }
    }
}
