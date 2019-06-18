using System;
using System.IO;
using System.Linq;
using System.Text;
using NameThatTuneBot.Database;
using NameThatTuneBot.MusicHandler.Converters;
using NameThatTuneBot.MusicHandler.Downloader;
using NameThatTuneBot.MusicHandler.Parser;
using NameThatTuneBot.MusicHandler.Parser.Interfaces;

namespace NameThatTuneBot.MusicHandler
{
    public class MusicITunesHandler:IMusicHandler
    {
        public MusicITunesHandler(IMusicTrackDatabase musicDatabase)
        {
            CreateFolder();
            this.musicDatabase = musicDatabase;
            musicParser = new MusicParserFacade(folderPath);
            musicDownloader = new MusicDownloaderITunes();
            musicConverters = new IMusicConverter[]
            {
                new MusicMp3Converter(folderPath),
                new MusicOggConverter(folderPath)
            };
        }

        private void CreateFolder()
        {
            var currentPath = Directory.GetCurrentDirectory();
            var newPath  = Path.Combine(currentPath, FolderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            folderPath = newPath;
        }


        public void AddTrackFromFile(string filePath)
        {
            Console.Out.WriteLine("Adding music started!");
            if(!File.Exists(filePath)) throw new FileNotFoundException(nameof(filePath));
            using (var sr = new StreamReader(filePath, Encoding.Unicode))
            {
                var s = string.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    var data = s.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 2 && int.TryParse(data[1], out var result))
                    {
                        HandleMusics(data[0], result);
                    }
                }

            }
            Console.Out.WriteLine("Adding music completed!");
        }

        private void HandleMusics(string searchText, int numberOfTracks = 1)
        {
            try
            {
                HandleRequest(searchText, numberOfTracks);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void HandleRequest(string searchText, int numberOfTracks = 1)
        {
            var musicTracks = musicParser.GetMusicTracks(searchText, numberOfTracks);

            var validMusicTracks = musicTracks.Where(mt => !musicDatabase.MusicTrackExist(mt)).ToArray();

            if (validMusicTracks.Length==0) return;

                musicDownloader.DownloadTrack(ref validMusicTracks, folderPath);
            foreach (var musicTrack in validMusicTracks)
            {
                var musicVersions = musicConverters.Select(x => x.ConvertTrack(musicTrack));
                musicTrack.MusicVersions.AddRange(musicVersions);
            }

            await musicDatabase.AddMusicTracksAsync(validMusicTracks);
        }
        internal void SetMusicTrackDatabase(IMusicTrackDatabase musicTrackDatabase)
        {
            this.musicDatabase = musicTrackDatabase;
        }

        internal void SetMusicDownloader(IMusicDownloader musicDownloader)
        {
            this.musicDownloader = musicDownloader;
        }

        internal void SetMusicParserFacade(IMusicParserFacade parserFacade)
        {
            this.musicParser = parserFacade;
        }

        internal void SetMusicConverters(IMusicConverter[] musicConverters)
        {
            this.musicConverters = musicConverters;
        }
        private string folderPath;
        private const  string FolderName = "MusicTracks";
        private IMusicTrackDatabase musicDatabase;
        private IMusicDownloader musicDownloader;
        private IMusicConverter[] musicConverters;
        private IMusicParserFacade musicParser;
    }
}