using Newtonsoft.Json;

namespace NationalityGame.Configuration.Settings.Sections
{
    public class AppearanceSettings
    {
        [JsonProperty(Required = Required.Always)]
        public double BucketWidth { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double BucketHeight { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double PhotoWidth { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double PhotoHeight { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int PhotoRunTimeInMs { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int TickIntervalInMs { get; set; }
    }
}