using System.IO;
using System.Text;

namespace _09_HW_GubinVS
{
    class Config
    {

        /// <summary>
        ///  Путь к файлу с токеном телеграмм
        /// </summary>
        private static string path = @"C:\teltoken.txt";

        /// <summary>
        /// Токет чат бота телеграмм, считывается из файла
        /// </summary>
        public static string Token { get;} = File.ReadAllText(path, Encoding.UTF8);

    
    }
}
