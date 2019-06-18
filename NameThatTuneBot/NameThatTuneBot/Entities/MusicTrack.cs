using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NameThatTuneBot.Entities
{
    public class MusicTrack
    {
        public int Id { get; set; }

        public string UriTrack { get; set; }

        public string NameArtist { get; set; }

        public string NameTrack { get; set; }

        public  List<MusicVersion> MusicVersions { get; set; }
    }
}