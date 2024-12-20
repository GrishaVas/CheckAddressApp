﻿namespace CheckAddressApp.Models.Here
{
    public class AutosuggestAddressRequest
    {
        public string Q { get; set; }
        public string At { get; set; }
        public string In
        {
            get
            {
                return @in;
            }
            set
            {
                @in = !string.IsNullOrEmpty(value) ? $"countryCode:{value}" : null;
            }
        }
        public string Ranking { get; set; }
        public string Route { get; set; }
        public List<string> With { get; set; }
        public List<string> Lang { get; set; }
        public List<int> Limit { get; set; }
        public List<int> Offset { get; set; }
        public string PoliticalView { get; set; }
        public List<string> Show { get; set; }

        private string @in;

        public AutosuggestAddressRequest(string q, string at)
        {
            Q = q;
            At = at;
        }
    }
}
