using NameThatTuneBot.Database;
using NameThatTuneBot.Entities;

namespace NameThatTuneBot.MusicHandler.Converters
{
    public interface IMusicConverter
    {
        MusicVersion ConvertTrack(MusicTrack musicTrack);
    }
}