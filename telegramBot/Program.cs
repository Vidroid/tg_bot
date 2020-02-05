using System.Threading;


namespace telegramBot
{
    class Program
    {
        public static TelegramMethods tM = new TelegramMethods();
       
        static void Main(string[] args)
        {
            tM.ResponseReceived += Router.OnResponse;
            Thread thread = new Thread(tM.GetUpdate);
            thread.IsBackground = true;
            thread.Start();
            while (true)
            {
                Thread.Sleep(5000);
            }
         
        }
    }
}
