using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NameThatTuneBot.Entities;
using NameThatTuneBot.MusicHandler.Parser.Interfaces;

namespace NameThatTuneBot.MusicHandler.Parser
{
    public class MusicParserFacade : IMusicParserFacade
    {
        public MusicParserFacade(string folderPath)
        {
            musicParser = new ResponseITunesParser();
            musicWebClient = new WebITunesClient();
        }

        public MusicTrack GetMusicTrack(string nameArtist, string nameTrack)
        {
            if (string.IsNullOrEmpty(nameTrack)) throw new ArgumentException();
            return GetMusicTracks(nameArtist, nameTrack, 1).First();
        }

        public MusicTrack[] GetMusicTracks(string nameArtist, int numberOfTracks = 1)
        {
            return GetMusicTracks(nameArtist, "", numberOfTracks);
        }

        private MusicTrack[] GetMusicTracks(string nameArtist, string nameTrack, int numberOfTracks)
        {
            if (string.IsNullOrEmpty(nameArtist) || numberOfTracks < 1)
            {
                throw new ArgumentException(nameof(nameArtist)+" or "+ nameof(numberOfTracks));
            }
            var addressRequest = new AddressITunesConstructor(nameArtist + ' ' + nameTrack, numberOfTracks);
            var httpResponse = musicWebClient.GetResponse(addressRequest);
            var musicTracks = musicParser.ParseResponseToTracks(httpResponse);
            if (musicTracks.Length == 0)
            {
                Console.Out.WriteLine($"Search = { nameArtist} { nameTrack} is empty");
                throw new Exception($"Search = { nameArtist}{nameTrack} is empty");
            }
            return musicTracks;
        }


        internal void SetMusicParser(IMusicParser musicParser)
        {
            this.musicParser = musicParser;
        }

        internal void SetMusicWebClient(IMusicWebClient musicWebClient)
        {
            this.musicWebClient = musicWebClient;
        }


        private IMusicParser musicParser;
        private IMusicWebClient musicWebClient;
    }
}