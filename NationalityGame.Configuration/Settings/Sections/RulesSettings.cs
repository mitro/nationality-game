using Newtonsoft.Json;

namespace NationalityGame.Configuration.Settings.Sections
{
    public class RulesSettings
    {
        [JsonProperty(Required = Required.Always)]
        public bool ShufflePhotos { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int CorrectChoiceScore { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int IncorrectChoiceScore { get; set; }
    }
}