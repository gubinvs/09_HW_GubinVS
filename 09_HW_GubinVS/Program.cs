using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Text.Json;

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
                var str = wc.DownloadString(Config.GetUpdates + update_id);
                //Console.WriteLine(str); 
                GetUpdates gu = JsonSerializer.Deserialize<GetUpdates>(str);

                if (gu.result.Length != 0)
                {
                    update_id = gu.result[0].update_id + 1;
                    
                    if (gu.result[0].message.text != null)
                    {
                        BotActions.PrintMessage(gu.result[0].message.text, gu.result[0].message.from.first_name);

                    }
                    else if (gu.result[0].file_id != null)
                    {
                        BotActions.DownloadFile(gu);
                    }

                }
                
                /// Необходимо переделать проверку CheckingInputParameters.ChekDocument
                /// Продолжаем проверять сообщения пока оно не обновиться, пустое сообщение приводит к ошибке десериализации





                Thread.Sleep(100);                                                                                   //  Остановка потока на 100 мс
            }

            



            Console.ReadKey();
        }
    }
}
