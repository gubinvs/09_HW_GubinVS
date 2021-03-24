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
                
                var str = wc.DownloadString(Config.GetUpdates + update_id);                                         // объявление переменной в которую помещается ответ полученный с сервера по запросу = getUpdates
                Console.WriteLine(str);
                GetUpdates gu = JsonSerializer.Deserialize<GetUpdates>(str);                                        // Объявляется переменная в  которую парсится полученный из запроса ответ в виде json файла
                Console.WriteLine(gu.result[0].update_id);
                update_id = gu.result[0].update_id + 1;                                                             // Идентификатор сообщение +1 (отмечается как прочитанное)

                //string userMessage = gu.result[0].message.text;
                //int userId = gu.result[0].message.from.id;
                //string useFirstrName = gu.result[0].message.from.first_name;
                    
                BotActions.PrintMessage(gu.result[0].message.text, gu.result[0].message.from.first_name);

                
                if (CheckingInputParameters.ChekDocument(gu))                                                       //  Проверка на наличие полей document
                {
                    BotActions.DownloadFile(gu);
                }




                /// Необходимо:
                /// готово =Ю Cоздать класс для десериализации первого сообщения GetUpdates
                /// готово  => Создать метод BotActions.DownloadFile = который будет выполнять работу по запросу пути к файлу и его скачивание в нужный каталог
                /// готово => Создать в Config = путь к папке каталогу для скачивания файлов PathDownloadFile


                /// Необходимо переделать проверку CheckingInputParameters.ChekDocument
                /// Продолжаем проверять сообщения пока оно не обновиться, пустое сообщение приводит к ошибке десериализации





                Thread.Sleep(100);                                                                                   //  Остановка потока на 100 мс
            }

            



            Console.ReadKey();
        }
    }
}
