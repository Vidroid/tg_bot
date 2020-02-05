using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace telegramBot
{
    class ParametrResponse : EventArgs
    {
        public string name="";
        public string username="";
        public string messageText="";
        public long chatId = 0;
        public long fromId = 0;
        public long replyUser = 0;
        public int replyMessage = 0;
        public int messageId = 0;


    }
}
