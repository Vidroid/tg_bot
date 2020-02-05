using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;

namespace telegramBot
{
    class TelegramMethods
    {
        private string _token;
        private string _url = "https://api.telegram.org/bot";
        public TelegramMethods(string Token)
        {
            this._token = Token;
        }

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

    }
}
