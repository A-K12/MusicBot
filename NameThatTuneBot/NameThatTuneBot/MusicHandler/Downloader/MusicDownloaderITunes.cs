using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NameThatTuneBot.Entities;
using NAudio.Wave;

namespace NameThatTuneBot.MusicHandler.Downloader
{
    internal class MusicDownloaderITunes:IMusicDownloader
    {
        public void DownloadTrack(ref MusicTrack[] musicTracks, string folderPath)
        {
            foreach (var track in musicTracks)
            {
                var trackPath = Path.Combine(folderPath, track.Id + ".wav");
                try
                {
                    TrimWavFile(track.UriTrack, trackPath, TimeSpan.FromSeconds(10));
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine("e. = {0}{1}", e.Message, e.HelpLink);
                    Console.WriteLine("DownloadFail=" + track.UriTrack.ToString());
                    trackPath = "";
                }

                var musicVersion = new MusicVersion {Extension = "wav", TrackPath = trackPath, MusicTrack = track};
                track.MusicVersions = new List<MusicVersion> {musicVersion};
            }

            if (musicTracks.Length < 1)
            {
                throw new Exception("");
            }
            musicTracks = musicTracks.Where(track => track.MusicVersions.Count()!= 0).ToArray();
        }

        private static void TrimWavFile(string inPath, string outPath, TimeSpan duration)
        {
            using (var reader = new MediaFoundationReader(inPath))
            {
                using (WaveFileWriter writer = new WaveFileWriter(outPath, reader.WaveFormat))
                {
                    var bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000f;

                    var startPos = 0;
                    startPos -= startPos % reader.WaveFormat.BlockAlign;

                    var endBytes = (int)Math.Round(duration.TotalMilliseconds * bytesPerMillisecond);
                    endBytes -= endBytes % reader.WaveFormat.BlockAlign;
                    var endPos = endBytes;

                    TrimWavFile(reader, writer, startPos, endBytes);
                }
            }
        }

        private static void TrimWavFile(MediaFoundationReader reader, WaveFileWriter writer, int startPos, int endPos)
        {
            reader.Position = startPos;
            var buffer = new byte[reader.BlockAlign * 1024];
            while (reader.Position < endPos)
            {
                var bytesRequired = (int)(endPos - reader.Position);
                if (bytesRequired <= 0) continue;
                var bytesToRead = Math.Min(bytesRequired, buffer.Length);
                var bytesRead = reader.Read(buffer, 0, bytesToRead);
                if (bytesRead > 0)
                {
                    writer.Write(buffer, 0, bytesRead);
                }
            }
        }
    }
}
