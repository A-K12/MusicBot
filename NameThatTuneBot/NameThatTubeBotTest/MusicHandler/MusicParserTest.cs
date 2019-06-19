using System;
using System.IO;
using System.Linq;
using NameThatTubeBotTest.MusicHandler.FakeObjects;
using NameThatTuneBot.MusicHandler.Parser;
using NUnit.Framework;

namespace NameThatTubeBotTest.MusicHandler
{
    [TestFixture]
    public class MusicParserTest
    {
        //[Test]
        //public void ParseResponseToTracks_RightJson_ReturnsTrack()
        //{
        //    var musicParser = new ResponseITunesParser();
        //    var JsonPath = @".\MusicHandler\TestExample.json";
        //    string jsonText;
        //    using (var r = new StreamReader(JsonPath))
        //    {
        //        jsonText = r.ReadToEnd();
        //    }

        //    var musicTrack = musicParser.ParseResponseToTracks(jsonText);

        //    Assert.AreEqual("artistName", musicTrack.First().NameArtist );
        //}

        [Test]
        public void ParseResponseToTracks_EmptyJson_ReturnsTrack()
        {
            var musicParser = new ResponseITunesParser();
            var JsonPath = @".\MusicHandler\EmptyExample.json";
            string jsonText;
            using (var r = new StreamReader(JsonPath))
            {
                jsonText = r.ReadToEnd();
            }

            var musicTrack = musicParser.ParseResponseToTracks(jsonText);

            Assert.AreEqual( 0, musicTrack.Length);
        }

        [Test]
        public void ParseResponseToTracks_SendNull_ReturnsTrack()
        {
            var musicParser = new ResponseITunesParser();

            var ex = Assert.Catch<Exception>(()=> musicParser.ParseResponseToTracks(null));

            StringAssert.Contains("Value cannot be null", ex.Message);
        }

    }
}