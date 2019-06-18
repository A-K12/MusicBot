using NameThatTubeBotTest.MusicHandler.FakeObjects;
using NameThatTuneBot;
using NameThatTuneBot.Messengers;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace NameThatTubeBotTest
{
    [TestFixture]
    public class MusicBotTest
    {
        [Test]
        public void Start_WhenCall_CallsBotMediator()
        {
            var messenger = new FakeMessengerApi();
            var musicBot = new MusicBot(new MessengerApi[]{messenger});
            var botMediator = new FakeBotMediator();
            musicBot.SetBotMediator(botMediator);

            musicBot.Start();

            Assert.True(botMediator.CheckStart);
        }

        [Test]
        public void Stop_WhenCall_CallsBotMediator()
        {
            var messenger = new FakeMessengerApi();
            var musicBot = new MusicBot(new MessengerApi[] { messenger });
            var botMediator = new FakeBotMediator();
            musicBot.SetBotMediator(botMediator);

            musicBot.Stop();

            Assert.True(botMediator.CheckStop);
        }

        [Test]
        public void AddMusicTracksFromFile_WhenCall_CallsMusicHandler()
        {
            var messenger = new FakeMessengerApi();
            var musicBot = new MusicBot(new MessengerApi[] { messenger });
            var musicHandler = new FakeMusicHandler();
            const string filePath = "Test";
            musicBot.SetMusicHandler(musicHandler);

            musicBot.AddMusicTracksFromFile(filePath);

            Assert.AreEqual(musicHandler.FilePath, filePath);
        }
    }
}