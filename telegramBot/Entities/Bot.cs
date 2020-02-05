using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace telegramBot.Entities
{
    class Bot
    {
        private static Chat chat;

        public int Bot_id { get; set; }
        public int Chat_id { get; set { this.Chat_id = chat.Chat_id; } } 
        public string Owner_name { get; set; }

        public Bot(int bot_id, string owner)
        {
            Bot_id = bot_id;
            Owner_name = owner;
        }
    }
}
