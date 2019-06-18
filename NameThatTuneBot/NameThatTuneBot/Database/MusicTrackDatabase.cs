using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NameThatTuneBot.Entities;


namespace NameThatTuneBot.Database
{
    public class MusicTrackDatabase:IMusicTrackDatabase
    {
        public MusicTrackDatabase()
        {

        }

        public async Task AddMusicTracksAsync(MusicTrack[] musicTracks)
        {
            if (musicTracks == null) throw new ArgumentNullException(nameof(musicTracks));

            foreach (var musicTrack in musicTracks)
            {
                try
                {
                   await AddMusicTrackAsync(musicTrack);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}-{musicTrack.NameArtist}--{musicTrack.NameTrack}");
                }
            }
        }

        public async Task AddMusicTrackAsync(MusicTrack musicTrack)
        {
            if (musicTrack?.MusicVersions == null) throw new ArgumentNullException(nameof(musicTrack));
            
            using (var dataBase = new NameThatTuneDatabase())
            {
                if (await dataBase.MusicTrack.AnyAsync(o => o.Id == musicTrack.Id))
                {
                    throw new Exception($"Track {musicTrack.NameArtist} - {musicTrack.NameTrack} already exists");
                };
                var musicVersions = musicTrack.MusicVersions.ToArray();
                musicTrack.MusicVersions = null;
                await dataBase.MusicTrack.AddAsync(musicTrack);
                await dataBase.SaveChangesAsync();
                await dataBase.MusicVersions.AddRangeAsync(musicVersions);
                await dataBase.SaveChangesAsync();
                Console.Out.WriteLine("DB: Track {0} added", musicTrack.NameArtist+"-"+musicTrack.NameTrack);
            }
        }

        public async Task<MusicTrack[]> GetRandomMusicTracksAsync(int numberOfTrack)
        {
            return await Task.Run(() => GetRandomMusicTracks(numberOfTrack));
        }

        private MusicTrack[] GetRandomMusicTracks(int numberOfTrack)
        {
            System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            myStopwatch.Start();
            using (var dataBase = new NameThatTuneDatabase())
            {
                var musicTracks = dataBase.MusicTrack.OrderBy(x => Guid.NewGuid()).Take(numberOfTrack).ToList();
                foreach (var musicTrack in musicTracks)
                {
                    dataBase.MusicVersions.Where(mv=>mv.MusicTrackId==musicTrack.Id).Load();
                }
                //OrderBy(x => Guid.NewGuid()).Include(t => t.MusicVersions)
                myStopwatch.Stop();
                Console.Out.WriteLine("DATABASE = {0}", myStopwatch.ElapsedMilliseconds);//остановить
                return musicTracks.ToArray();
            }

        }

        public MusicTrack GetMusicTrack(int trackId)
        {
            using (var dataBase = new NameThatTuneDatabase())
            {
                return dataBase.MusicTrack.Include(t => t.MusicVersions).
                        SingleOrDefault(m => m.Id == trackId);
            }
        }

        public bool MusicTrackExist(MusicTrack musicTrack)
        {
            using (var dataBase = new NameThatTuneDatabase())
            {
                return dataBase.MusicTrack.Any(mt => mt.Id == musicTrack.Id);
            }
        }
    }
}