using Newtonsoft.Json;

namespace NationalityGame.Configuration.Settings.Sections
{
    public class PhotoSettings
    {
        [JsonProperty(Required = Required.Always)]
        public string Nationality { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string ImagePath { get; set; }
    }
}