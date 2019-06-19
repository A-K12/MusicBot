using Newtonsoft.Json;

namespace MusicBot
{
    public class Keys
    {
        [JsonProperty("telegram")]
        public string Telegram { get; set; }
        [JsonProperty("discord")]
        public string Discord { get; set; }
    }
}