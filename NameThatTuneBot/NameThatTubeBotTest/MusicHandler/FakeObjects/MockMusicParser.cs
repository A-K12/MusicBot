﻿using NameThatTuneBot.Entities;
using NameThatTuneBot.MusicHandler.Parser.Interfaces;

namespace BotTest.MusicHandler
{
    public class MockMusicParser:IMusicParser
    {
        public MusicTrack[] MusicTracks { get; set; } = new MusicTrack[0];
        public MusicTrack[] ParseResponseToTracks(string response)
        {
            return MusicTracks;
        }
    }
}