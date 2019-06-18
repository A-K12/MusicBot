using System.Linq;
using NameThatTuneBot.Entities;
using NameThatTuneBot.MusicHandler.Parser.Interfaces;

namespace BotTest.MusicHandler
{
    public class FakeMusicParserFacade:IMusicParserFacade
    {
        public string NameArtist { get; set; }
        public string NameTrack { get; set; }
        public int NumberOfTrack { get; set; }
        public MusicTrack MusicTrack { get; set; }
        public MusicTrack GetMusicTrack(string nameArtist, string nameTrack)
        {
            var searchText = nameArtist + " " + nameTrack;
            return GetMusicTracks(searchText, 1).First();
        }

        public MusicTrack[] GetMusicTracks(string nameArtist, int numberOfTracks)
        {
            NameArtist = nameArtist;
            NumberOfTrack = numberOfTracks;
            return new MusicTrack[]{ MusicTrack};
        }
    }
}