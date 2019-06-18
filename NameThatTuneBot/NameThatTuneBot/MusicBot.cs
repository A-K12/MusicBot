using System.Collections.Generic;
using NameThatTuneBot.Database;

namespace NameThatTuneBot
{
    public class MusicBot
    {
        public MusicBot(IEnumerable<MessengerApi> messengers)
        {
            musicDatabase = new MusicTrackDatabase();
            botBotMediator = new BotMediator(musicDatabase);
            foreach (var messenger in messengers)
            {
                botBotMediator.AddMessenger(messenger); 
            }
        }

        public void Start()
        {
            botBotMediator.Start();
        }

        public void Stop()
        {
            botBotMediator.Stop();
        }

        internal void SetBotFacade(IBotMediator botBotMediator)
        {
            this.botBotMediator = botBotMediator;
        }


        internal void SetDatabase(IMusicTrackDatabase musicDatabase)
        {
            this.musicDatabase = musicDatabase;
        }

        private IMusicTrackDatabase musicDatabase;
        private IBotMediator botBotMediator;
    }
}