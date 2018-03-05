using System.Collections.Generic;
using NationalityGame.Configuration.Settings.Sections;
using Newtonsoft.Json;

namespace NationalityGame.Configuration.Settings
{
    public class GameSettings
    {
        [JsonProperty(Required = Required.Always)]
        public RulesSettings Rules { get; set; }

        [JsonProperty(Required = Required.Always)]
        public BucketsSettings Buckets { get; set; }

        [JsonProperty(Required = Required.Always)]
        public IEnumerable<PhotoSettings> Photos { get; set; }

        [JsonProperty(Required = Required.Always)]
        public AppearanceSettings Appearance { get; set; }
    }
}