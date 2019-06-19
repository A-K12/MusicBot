using System.Linq;
using System.Threading.Tasks;
using NameThatTuneBot.Database;
using NameThatTuneBot.Entities;

namespace NameThatTubeBotTest.FakeObjects
{
    public class FakeMusicDatabase:IMusicTrackDatabase
    {
        public MusicTrack MusicTrack { get; set; }
        public MusicTrack[] AddedMusicTracks { get; set; }
        public bool ExistenceMusic { get; set; }

        public Task AddMusicTracksAsync(MusicTrack[] musicTracks)
        {
            this.AddedMusicTracks= musicTracks;
            return  Task.CompletedTask;
        }

        public Task AddMusicTrackAsync(MusicTrack musicTrack)
        {
            this.MusicTrack = musicTrack;
            return Task.CompletedTask;
        }

        public Task<MusicTrack[]> GetRandomMusicTracksAsync(int numberOfTrack)
        {
            var tracks = Enumerable.Repeat<MusicTrack>(MusicTrack, numberOfTrack).ToArray();
            return Task.FromResult(tracks);
        }

        public MusicTrack GetMusicTrack(int trackId)
        {
            return MusicTrack;
        }

        public bool MusicTrackExist(MusicTrack musicTrack)
        {
            return ExistenceMusic;
        }

        public Task UpdateUserStatistics(User user, bool answerType)
        {
            throw new System.NotImplementedException();
        }

        public Task AddUserStatistics(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}