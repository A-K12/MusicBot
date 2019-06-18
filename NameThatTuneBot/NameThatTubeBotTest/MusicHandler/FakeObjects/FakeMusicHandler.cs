using NameThatTuneBot.MusicHandler;

namespace BotTest.MusicHandler
{
    public class FakeMusicHandler:IMusicHandler
    {
        public  string FilePath { get; set; }
        public void AddTrackFromFile(string filePath)
        {
            FilePath = filePath;
        }
    }
}