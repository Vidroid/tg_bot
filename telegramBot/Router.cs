using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace telegramBot
{
    class Router
    {
        public static void OnResponse(object sender, ParametrResponse e)
        {

            if (e.messageText == "/olo")
            {
                // tM.PinMessage(e.chatId, e.messageId);
            }
            Console.WriteLine("{0}: {1}", e.chatId, e.messageText);
        }
    }
}
