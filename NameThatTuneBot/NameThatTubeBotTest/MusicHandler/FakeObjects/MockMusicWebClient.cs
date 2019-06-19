using System.IO;
using NameThatTuneBot.MusicHandler.Parser;
using NameThatTuneBot.MusicHandler.Parser.Interfaces;

namespace NameThatTubeBotTest.MusicHandler.FakeObjects
{
    public class MockMusicWebClient:IMusicWebClient
    {
        public  string JsonPath { get; set; }
        public string GetResponse(AddressITunesConstructor address)
        {
            using (var r = new StreamReader(JsonPath))
            {
                return r.ReadToEnd();
            }
        }
    }
}