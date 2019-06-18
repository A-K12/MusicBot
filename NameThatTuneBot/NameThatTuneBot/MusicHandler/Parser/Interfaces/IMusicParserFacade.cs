using NameThatTuneBot.Database;
using NameThatTuneBot.Entities;

namespace NameThatTuneBot.MusicHandler.Parser.Interfaces
{
    public interface IMusicParserFacade
    {
        MusicTrack GetMusicTrack(string nameArtist, string nameTrack);
        MusicTrack[] GetMusicTracks(string nameArtist, int numberOfTracks );
    }
}
