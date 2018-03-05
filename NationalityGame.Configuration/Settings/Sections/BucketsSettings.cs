using Newtonsoft.Json;

namespace NationalityGame.Configuration.Settings.Sections
{
    public class BucketsSettings
    {
        [JsonProperty(Required = Required.Always)]
        public BucketSettings TopLeft { get; set; }

        [JsonProperty(Required = Required.Always)]
        public BucketSettings TopRight { get; set; }

        [JsonProperty(Required = Required.Always)]
        public BucketSettings BottomRight { get; set; }

        [JsonProperty(Required = Required.Always)]
        public BucketSettings BottomLeft { get; set; }
    }
}