using Newtonsoft.Json.Linq;

namespace _09_HW_GubinVS
{
    class CheckingInputParameters
    {
        /// <summary>
        /// Метод принимает массив данных, проверят наличие поля документ
        /// возвращает true если такое поле есть, в противном случае filse
        /// </summary>
        public static bool ChekDocument(dynamic ob)
        {
            if (ob.message.document != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод принимает текст сообщения и проверяет приветствие ли это
        /// </summary>
        /// <param name="userMessage"></param>
        /// <returns></returns>
        public static bool ChekMessageHi(string userMessage)
        {
            if (userMessage == "hi")
            {
                return true;
            }
            return false;
        }

    }
}
