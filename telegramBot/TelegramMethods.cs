using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using SimpleJSON;

namespace telegramBot
{
    class TelegramMethods
    {

        private string _token = "";
        private string _url = "https://api.telegram.org/bot";
        private int lastUpdateId = 0;
        public delegate void Response(object Sender, ParametrResponse e);
        public event Response ResponseReceived;
        ParametrResponse e = new ParametrResponse();

        public string GetMe()
        {
            using (var webClient = new WebClient())
            {
                return webClient.DownloadString(_url+_token+"/getMe");
            }
        }

        public void SendMessage(string message, long id)
        {
            var pars = new NameValueCollection();
            pars.Add("text", message);
            pars.Add("chat_id", id.ToString());
            using (var webClient = new WebClient())
            {
                webClient.UploadValues(_url + _token + "/sendMessage", pars);
            }
        }

        public void EditMessage(string message, long id, long messageId)
        {
            var pars = new NameValueCollection();
            pars.Add("text", message);
            pars.Add("message_id", messageId.ToString());
            pars.Add("chat_id", id.ToString());
            using (var webClient = new WebClient())
            {
                webClient.UploadValues(_url + _token + "/editMessage", pars);
            }
        }

        public void PinMessage(long id, long messageId)
        {
            var pars = new NameValueCollection();
            pars.Add("disable_notification", false.ToString());
            pars.Add("message_id", messageId.ToString());
            pars.Add("chat_id", id.ToString());
            using (var webClient = new WebClient())
            {
                webClient.UploadValues(_url + _token + "/pinChatMessage", pars);
            }
        }

        public void UnPinMessage(long id)
        {
            var pars = new NameValueCollection();
            pars.Add("chat_id", id.ToString());
            using (var webClient = new WebClient())
            {
                webClient.UploadValues(_url + _token + "/unpinChatMessage", pars);
            }
        }

        public void GetUpdate()
        {
            while (true)
            {
                using (WebClient webClient = new WebClient())
                {
                    string response = webClient.DownloadString("https://api.telegram.org/bot" + _token + "/getUpdates?offset=" + (lastUpdateId + 1));
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
                ResponseReceived(this, e);
            }
        }

    }
}
