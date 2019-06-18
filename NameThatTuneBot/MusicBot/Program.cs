using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NameThatTuneBot;
using NameThatTuneBot.Messengers;

namespace MusicBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var keys = GetKeys(@"D:\Keys.txt");
            var telegram = new TelegramApi(keys["telegram"]);
            var discord = new DiscordApi(keys["discord"]);
            var bot = new NameThatTuneBot.MusicBot(new MessengerApi[] { telegram, discord });
            try
            {
                bot.AddMusicTracksFromFile(@"D:\1.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            bot.Start();
            Console.ReadKey();
            bot.Stop();
        }

        private static Dictionary<string, string> GetKeys(string path)
        {
            var keys = new Dictionary<string, string>();
            using (var sr = new StreamReader(path, Encoding.Unicode))
            {
                var s = string.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    var data = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    keys.Add(data[0], data[1]);
                }
            }

            return keys;
        }
    }
}