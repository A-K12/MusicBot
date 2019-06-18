using System.Collections.Generic;
using NameThatTuneBot.Database;
using NameThatTuneBot.MusicHandler;

namespace NameThatTuneBot
{
    public class MusicBot
    {
        public MusicBot(IEnumerable<MessengerApi> messengers)
        {
            musicDatabase = new MusicTrackDatabase();
            musicHandler = new MusicITunesHandler(musicDatabase);
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

        public void AddMusicTracksFromFile(string filePath)
        {
            musicHandler.AddTrackFromFile(filePath);
        }


        internal void SetBotFacade(IBotMediator botBotMediator)
        {
            this.botBotMediator = botBotMediator;
        }


        internal void SetDatabase(IMusicTrackDatabase musicDatabase)
        {
            this.musicDatabase = musicDatabase;
        }

        internal void SetMusicHandler(IMusicHandler musicHandler)
        {
            this.musicHandler = musicHandler;
        }

        private IMusicTrackDatabase musicDatabase;
        private IBotMediator botBotMediator;
        private IMusicHandler musicHandler;
    }
}