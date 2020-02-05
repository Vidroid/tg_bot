using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleJSON;
using System.Net;
using System.Threading;
//using telegramBot.ParametrResponse;


namespace telegramBot
{
    class Program
    {
        public static TelegramResponse tR = new TelegramResponse();
        public static TelegramMethods tM = new TelegramMethods(tR.token);

        static void Main(string[] args)
        {
            TelegramResponse Tr = new TelegramResponse();
            
            
           Tr.ResponseReceived += OnResponse;
            Thread thread = new Thread(Tr.GetUpdate);
            thread.IsBackground = true;
            thread.Start();
            while (true)
            {
                Thread.Sleep(5000);
            }
         
        }
        
        public static void OnResponse(object sender, ParametrResponse e, TelegramMethods tM)
        {
            
            if (e.messageText == "/olo")
            {
                tM.PinMessage(e.chatId, e.messageId);
            }
            Console.WriteLine("{0}: {1}", e.chatId, e.messageText);
        }

        public delegate void Response(object Sender, ParametrResponse e, TelegramMethods tM);

       
       public class TelegramResponse
        {
            public string token = "843645523:AAEfttRTr3fLocGP0wmT-V11PYFGC5Ro9qQ";
            int lastUpdateId = 0;
            public event Response ResponseReceived;
            ParametrResponse e = new ParametrResponse();

            public void GetUpdate()
            {
                while (true)
                {
                    using (WebClient webClient = new WebClient())
                    {
                       string response = webClient.DownloadString("https://api.telegram.org/bot"+token+"/getUpdates?offset="+(lastUpdateId+1));
                       if (response.Length <= 23)
                       {
                           continue;
                       }
                        var N = JSONNode.Parse(response);
                       foreach (JSONNode r in N["result"].AsArray)
                       {
                           lastUpdateId = r["update_id"].AsInt;
                           e.username = r["message"]["from"]["username"];
                           e.messageText = r["message"]["text"];
                           e.chatId = r["message"]["chat"]["id"].AsInt;
                           e.messageId = r["message"]["message_id"].AsInt;
                           e.replyMessage = r["message"]["reply_to_message"]["message_id"].AsInt;
                           e.replyUser = r["message"]["reply_to_message"]["from"]["id"].AsInt;
                           e.fromId = r["message"]["from"]["id"].AsInt;
                           
                       }
                    }
                    ResponseReceived(this, e, tM);
                }
            }
        }
    }
}
