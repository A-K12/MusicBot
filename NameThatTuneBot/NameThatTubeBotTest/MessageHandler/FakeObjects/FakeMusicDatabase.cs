using System.Threading.Tasks;
using NameThatTuneBot.Database;
using NameThatTuneBot.Entities;

namespace BotTest.MockObjects
{
    public class FakeMusicDatabase:IMusicTrackDatabase
    {
        public MusicTrack MusicTrack { get; set; }
        public MusicTrack[] AddedMusicTracks { get; set; }
        public bool ExistenceMusic { get; set; }

        public MusicTrack AddedMusicTrack { get; set; }

        public Task AddMusicTracksAsync(MusicTrack[] musicTracks)
        {
            this.AddedMusicTracks = musicTracks;
            return  Task.CompletedTask;
        }

        public Task AddMusicTrackAsync(MusicTrack musicTrack)
        {
            this.MusicTrack = musicTrack;
            return Task.CompletedTask;
        }

        public Task<MusicTrack[]> GetRandomMusicTracksAsync(int numberOfTrack)
        {
            return Task.FromResult(AddedMusicTracks);
        }

        public MusicTrack GetMusicTrack(int trackId)
        {
            return MusicTrack;
        }

        public bool MusicTrackExist(MusicTrack musicTrack)
        {
            return ExistenceMusic;
        }
    }
}