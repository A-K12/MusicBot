
using NameThatTuneBot.Entities;

namespace NameThatTuneBot.MusicHandler.Parser.Interfaces
{
    public interface IMusicParser
    {
        MusicTrack[] ParseResponseToTracks(string response);
    }
}
