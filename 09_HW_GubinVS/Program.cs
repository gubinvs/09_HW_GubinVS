using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Text.Json;
using System.Collections.Generic;

namespace _09_HW_GubinVS
{

                        
                                                // Бот обладает следующим набором функций:
                                                // Принимает сообщения от пользователя
                                                // Принимает команды от пользователя.
                                                // Сохраняет аудиосообщения, картинки и произвольные файлы.
                                                // Позволяет пользователю просмотреть список загруженных файлов.
                                                // Позволяет скачать выбранный файл.


    class Program
    {
        static void Main(string[] args)
        {
            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };                                                            // экземпляр класса работы с сетью
            // Список полученных документов
            List<Doc> document = new List<Doc>();

            int update_id = 0;                                                                                                      // возвращаемый номер сообщения обработанного методом по умолчанию = 0
            
            while (true)                                                                                                            // Цикл бесконечно отправляет запросы серверу Telegram
            {
                var str = wc.DownloadString(Config.GetUpdates + update_id);
                //Console.WriteLine(str);
                
                GetUpdates gu = JsonSerializer.Deserialize<GetUpdates>(str);                                                        // Дисериализация входящего сообщения в класс GetUpdates

                if (gu.result.Any(x => x.update_id != 0))                                                                           // проверка не пустое ли сообщение
                {
                    update_id = gu.result[0].update_id + 1;                                                                         // Прибавляем еденицу к текущему сообщению (отметили как прочитанное)

                    if (gu.result.Any(x => x.message.text != null))
                    {
                        BotActions.PrintMessage(gu.result[0].message.text, gu.result[0].message.from.first_name);                   // если в сообщении есть текст => вывести его в консоль
                        BotActions.SendMessageText(wc, gu);                                                                         // отвечает на сообщения
                      
                    }
                    else if (gu.result.Any(x => x.message.document != null))                                                        // если есть в сообщении документ 
                    {
                        BotActions.DownloadFile(gu);                                                                                // Скачивает файл на диск
                        // заполняем список с данными о загруженных файлах
                        document.Add(new Doc 
                        { 
                            File_id = gu.result[0].message.document.file_id,
                            File_name = gu.result[0].message.document.file_name
                        
                        });

                        foreach (var item in document)
                        {
                            Console.WriteLine(item.File_name);
                        }
                    }
                    else if (gu.result.Any(x => x.message.photo != null))                                                           // если есть в сообщении Photo
                    {
                        BotActions.DownloadFoto(gu);
                    }
                    else if (gu.result.Any(x => x.message.sticker != null))                                                         // если есть в сообщении Sticker
                    {
                        BotActions.DownloadSticker(gu);
                    }
                    else if (gu.result.Any(x => x.message.voice != null))                                                           //  если есть в сообщении голосовое сообщение
                    {
                        BotActions.DownloadVoice(gu);
                    }
                    else if (gu.result.Any(x => x.message.video_note != null))                                                      // если есть в сообщении видео сообщение
                    {
                        BotActions.DownloadVideo(gu);
                    }

                }

                Thread.Sleep(100);                                                                                                  //  Остановка потока на 100 мс
            }

            // Необходимо решить следующую задачу:
            // При загрузке файла , необходимо создать некий список в котором хранить file_id полученного файла. А при выборе пользователем скачивание этого файла идентифицировать его 
            // и отправить ссылку пользователю.


            // Удалить потом
            Console.ReadKey();
        }
    }
}
