using NameThatTuneBot.MusicHandler;

namespace NameThatTubeBotTest.MusicHandler.FakeObjects
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