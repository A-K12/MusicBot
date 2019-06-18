using System;
using System.Net.Http;
using NameThatTuneBot.MusicHandler.Parser.Interfaces;

namespace NameThatTuneBot.MusicHandler.Parser
{
    public class WebITunesClient:IMusicWebClient
    {
        public string GetResponse(AddressITunesConstructor address)
        {
            using (var client = new HttpClient())
            {
                var httpResponse = client.GetAsync(address.GetAddressRequest()).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    return httpResponse.Content.ReadAsStringAsync().Result; 
                }
                else
                {
                    throw new Exception("connection problem");
                }
            }
        }
    }
}