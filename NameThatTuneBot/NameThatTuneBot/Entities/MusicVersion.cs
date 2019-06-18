using System;

namespace NameThatTuneBot.Entities
{
    public class MusicVersion
    {
        public  int Id { get; set; }
        public string TrackPath { get; set; }
        public  string Extension { get; set; }
        public int MusicTrackId { get; set; }
        public MusicTrack MusicTrack { get; set; }
    }
}