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
            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };                                            // экземпляр класса работы с сетью
            
            int update_id = 0;                                                                                      // возвращаемый номер сообщения обработанного методом по умолчанию = 0
            
            while (true)                                                                                            // Цикл бесконечно отправляет запросы серверу Telegram
            {
                var str = wc.DownloadString(Config.GetUpdates + update_id);                                                                   // объявление переменной в которую помещается ответ полученный с сервера по запросу = getUpdates
                Console.WriteLine(str);
                var msgs = JObject.Parse(str)["result"].ToArray();                                                  // Объявляется переменная в  которую парсится полученный из запроса ответ в виде json файла

                foreach (dynamic msg in msgs)                                                                       // цикл перебирает масссив созданный из полученного файла json при получение
                {
                    update_id = Convert.ToInt32(msg.update_id) + 1;

                    string userMessage = msg.message.text;
                    string userId = msg.message.from.id;
                    string useFirstrName = msg.message.from.first_name;
                    BotActions.PrintMessage(userMessage, useFirstrName);


                    if (CheckingInputParameters.ChekDocument(msg))                                                  //  Проверка на наличие полей document
                    {
                        string file_id = msg.message.document.file_id;
                        Console.WriteLine(Config.GetFile + file_id);
                        var w = wc.DownloadString(Config.GetFile + file_id);
                        Console.Write(w);
                        var msgs2 = JObject.Parse(w);
                        foreach (dynamic item in msgs2)
                        {
                            string file_path = item.file_size;
                            Console.WriteLine(file_path);
                        }

                    }


                }

                








                Thread.Sleep(100);                                                                                   //  Остановка потока на 100 мс
            }

            



            Console.ReadKey();
        }
    }
}
