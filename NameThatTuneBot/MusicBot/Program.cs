using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NameThatTuneBot;
using NameThatTuneBot.Messengers;
using Newtonsoft.Json;


namespace MusicBot
{
    static class Program
    {
        static void Main()
        {
            try
            {
                InitializeBot();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadKey();
        }
        private static void InitializeBot()
        {
            var keys = GetKeys(@".\Keys.json");
            var telegram = new TelegramApi(keys.Telegram);
            var discord = new DiscordApi(keys.Discord);
            var bot = new NameThatTuneBot.MusicBot(new MessengerApi[] { telegram, discord });
            try
            {
                bot.AddMusicTracksFromFile(@".\1.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            bot.Start();
            Console.ReadKey();
            bot.Stop();
        }
        private static Keys GetKeys(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException(nameof(path));
            string jsonText;
            using (var r = new StreamReader(path))
            {
               jsonText= r.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<Keys>(jsonText);
        }
    }
}