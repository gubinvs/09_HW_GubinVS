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
                Console.WriteLine(str);
                
                GetUpdates gu = JsonSerializer.Deserialize<GetUpdates>(str);                                        // Дисериализация входящего сообщения в класс GetUpdates

                if (gu.result.Any(x => x.update_id != 0))                                                           // проверка не пустое ли сообщение
                {
                    update_id = gu.result[0].update_id + 1;                                                         // Прибавляем еденицу к текущему сообщению (отметили как прочитанное)

                    if (gu.result.Any(x => x.message.text != null))
                    {
                        BotActions.PrintMessage(gu.result[0].message.text, gu.result[0].message.from.first_name);   // если в сообщении есть текст => вывести его в консоль
                    }
                    else if (gu.result.Any(x => x.message.document != null))                                        // если есть в сообщении документ 
                    {
                        BotActions.DownloadFile(gu);                                                                // Скачивает файл на диск
                    }
                    else if (gu.result.Any(x => x.message.photo.Length != 0))
                    {
                        BotActions.DownloadFoto(gu);
                        
                    }

                }

                





                Thread.Sleep(100);                                                                                   //  Остановка потока на 100 мс
            }

            


            // Удалить потом
            Console.ReadKey();
        }
    }
}
