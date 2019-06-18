using System;
using System.IO;
using System.Linq;
using NameThatTuneBot.Entities;
using NAudio.Wave;


namespace NameThatTuneBot.MusicHandler.Converters
{
    internal class MusicMp3Converter:IMusicConverter
    {
        public MusicMp3Converter(string folderName)
        {
            this.folderPath = folderName;
        }
    
        public MusicVersion ConvertTrack(MusicTrack musicTrack)
        {
            var mp3Path = Path.Combine(folderPath, musicTrack.Id + ".mp3");
            var wavPath = musicTrack.MusicVersions.SingleOrDefault(p => p.Extension == "wav");
            using (var reader = new WaveFileReader(wavPath.TrackPath))
            {
                try
                {
                    MediaFoundationEncoder.EncodeToMp3(reader, mp3Path);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return new MusicVersion() { MusicTrack = musicTrack, TrackPath = mp3Path, Extension = "mp3" };
        }

        private short[] BytesToShorts(byte[] bytes)
        {
            var processedValues = new short[bytes.Length/ 2];
            for (var c = 0; c < processedValues.Length; c++)
            {
                processedValues[c] = (short)(((int)bytes[(c * 2) ]) << 0);
                processedValues[c] += (short)(((int)bytes[(c * 2) + 1 ]) << 8);
            }

            return processedValues;
        }

        internal  void SetFolderPath(string path)
        {
            folderPath = path;
        }

        private string folderPath;
    }

}
