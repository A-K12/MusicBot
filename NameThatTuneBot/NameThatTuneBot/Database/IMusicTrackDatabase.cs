using System.Threading.Tasks;
using NameThatTuneBot.Entities;

namespace NameThatTuneBot.Database
{
    public interface IMusicTrackDatabase
    {
        Task AddMusicTracksAsync(MusicTrack[] musicTracks);
        Task AddMusicTrackAsync(MusicTrack musicTrack);

        Task<MusicTrack[]> GetRandomMusicTracksAsync(int numberOfTrack);

        MusicTrack GetMusicTrack(int trackId);
        bool MusicTrackExist(MusicTrack musicTrack);
    }
}