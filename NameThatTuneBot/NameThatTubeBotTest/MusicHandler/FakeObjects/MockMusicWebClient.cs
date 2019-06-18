using NameThatTuneBot.MusicHandler.Parser;
using NameThatTuneBot.MusicHandler.Parser.Interfaces;

namespace BotTest.MusicHandler
{
    public class MockMusicWebClient:IMusicWebClient
    {
        public  string Response { get; set; }
        public string GetResponse(AddressITunesConstructor address)
        {
            return Response;
        }
    }
}