using System;
using System.Collections.Generic;
using NameThatTuneBot.Entities;
using NameThatTuneBot.MusicHandler.Downloader;

namespace BotTest.MusicHandler
{
    public class FakeMusicDownloader:IMusicDownloader
    {
        public string FolderPath { get; set; }

        public MusicVersion MusicVersion { get; set; } = new MusicVersion();

        public void DownloadTrack(ref MusicTrack[] musicTracks, string folderPath)
        {
            FolderPath = folderPath;
            foreach (var musicTrack in musicTracks)
            {
                musicTrack.MusicVersions = new List<MusicVersion>() {MusicVersion};
            }    
        }
    }
}