using System;
using System.Collections.Generic;
using System.Text;
using  System.Linq;

namespace NameThatTuneBot.MusicHandler.Parser
{
    public class AddressITunesConstructor
    {
        private const string MainAddress = "https://itunes.apple.com/search?parameterkeyvalue";
        private readonly string term;
        private readonly int limits;

        public AddressITunesConstructor(string term, int limits)
        {
            var termsSearch = term.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var termLine = string.Join("+", termsSearch);
            this.term = termLine;
            this.limits = limits;
        }

        public string GetAddressRequest()
        {
            return MainAddress + "&term=" + term + "&media=music&limit=" + limits.ToString();
        }
    }

}

