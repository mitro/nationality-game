using Newtonsoft.Json;

namespace NationalityGame.Configuration.Settings.Sections
{
    public class ScoringSettings
    {
        [JsonProperty(Required = Required.Always)]
        public int CorrectPoints { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int IncorrectPoints { get; set; }
    }
}