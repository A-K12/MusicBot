using NameThatTuneBot.Database;
using NameThatTuneBot.MusicHandler.Converters;
using NameThatTuneBot.MusicHandler.Downloader;
using NameThatTuneBot.MusicHandler.Parser.Interfaces;

namespace NameThatTuneBot.MusicHandler
{
    public interface IMusicHandler
    {
        void AddTrackFromFile(string filePath);
    }
}