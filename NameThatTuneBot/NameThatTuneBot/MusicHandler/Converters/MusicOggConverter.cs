using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Concentus.Enums;
using Concentus.Oggfile;
using Concentus.Structs;
using NameThatTuneBot.Entities;
using NAudio.Wave;

namespace NameThatTuneBot.MusicHandler.Converters
{
    internal class MusicOggConverter:IMusicConverter
    {
        public MusicOggConverter(string folderPath)
        {
            this.folderPath = folderPath;
        }

        public MusicVersion ConvertTrack(MusicTrack musicTrack)
        {
            var path = Path.Combine(folderPath, musicTrack.Id + ".ogg");
            var wavPath = musicTrack.MusicVersions.SingleOrDefault(p => p.Extension == "wav");
            byte[] bytes = ReadAllBytes(wavPath.TrackPath);

            using (var fileOut = new FileStream(path, FileMode.Create))
            {

                var encoder = OpusEncoder.Create(48000, 2, OpusApplication.OPUS_APPLICATION_AUDIO);
                var oggOut = new OpusOggWriteStream(encoder, fileOut);

                //byte[] allInput = File.ReadAllBytes(wavPath.TrackPath);
                short[] samples = BytesToShorts(bytes);

                oggOut.WriteSamples(samples, 0, samples.Length);
                oggOut.Finish();
            }

            return new MusicVersion() {MusicTrack = musicTrack, TrackPath = path, Extension = "ogg"};
        }

        private byte[] ReadAllBytes(string filePath)
        {
            var reader = new MediaFoundationReader(filePath);
            var newFormat = new WaveFormat(48000, reader.WaveFormat.Channels);
            var newStream = new WaveFormatConversionStream(newFormat, reader);
            var conv = WaveFormatConversionStream.CreatePcmStream(newStream);
            var bytes = new byte[conv.Length];
            conv.Position = 0;
            conv.Read(bytes, 0, (int)conv.Length);
            return bytes;
        }

        private short[] BytesToShorts(byte[] bytes)
        {
            List<short> shorts = new List<short>();
            int i = 0;
            while (i < bytes.Length)
            {
                shorts.Add(BitConverter.ToInt16(new byte[2] { bytes[i], bytes[i + 1] }, 0));
                i = i + 2;
            }
            return shorts.ToArray();
        }

        internal  void SetFolderPath(string path)
        {
            folderPath = path;
        }

        private string folderPath;
    }

}
