using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace telegramBot.Entities
{
    class Chat
    {
        private static Game game;
        public int Chat_id { get; set; }
        private int Game_id { get; set; } = game.Game_id;
    }
}
