using System;
using NameThatTuneBot.Entities;


namespace NameThatTuneBot.MusicHandler.Downloader
{
    public interface IMusicDownloader
    {
        void DownloadTrack(ref MusicTrack[] musicTracks, string folderPath);
    }
}
