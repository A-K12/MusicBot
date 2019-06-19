using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Linq;
using NameThatTuneBot.Entities;
using NameThatTuneBot.MusicHandler.Parser.Interfaces;
using  Newtonsoft.Json.Linq;


namespace NameThatTuneBot.MusicHandler.Parser
{
    internal class ResponseITunesParser: IMusicParser
    {
        public MusicTrack[] ParseResponseToTracks(string response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            var musicTracks = new List<MusicTrack>();
            var block  = JObject.Parse(response);
            var results = block["results"].Where(x => x["kind"].ToString() == "song").ToArray();
            if (results.Length == 0)
            {
                return new MusicTrack[0];
            }
            foreach (var token in results)
            {
                var trackUrl = token["previewUrl"].ToString();
                if (!IsValidUrl(trackUrl))
                {
                    Console.Out.WriteLine("trackUrl is invalid = {0}", trackUrl);
                    continue;
                }
                var track = new MusicTrack()
                {
                    NameArtist = token["artistName"].ToString(),
                    NameTrack = token["trackName"].ToString(),
                    Id = int.Parse(token["trackId"].ToString()),
                    UriTrack = trackUrl
                };
                musicTracks.Add(track);
            }

            return musicTracks.ToArray();
        }

        private static bool IsValidUrl(string uriName)
        {
            return Uri.TryCreate(uriName, UriKind.Absolute, out var uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

    }
}