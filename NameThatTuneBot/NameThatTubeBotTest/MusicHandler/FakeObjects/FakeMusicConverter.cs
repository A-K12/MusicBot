using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using NameThatTuneBot.Entities;
using NameThatTuneBot.MusicHandler.Converters;

namespace NameThatTubeBotTest.MusicHandler.FakeObjects
{
    public class FakeMusicConverter:IMusicConverter
    {
        public MusicVersion Version { get; set; } = new MusicVersion();
        public  MusicTrack AddedTrack { get; set; }
        public MusicVersion ConvertTrack(MusicTrack musicTrack)
        {
            AddedTrack = musicTrack;
            return Version;
        }
    }
}