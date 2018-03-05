using System.Collections.Generic;

namespace NationalityGame.Mechanics.Configuration
{
    public class GameSettings
    {
        public BucketsSettings Buckets { get; set; }

        public IEnumerable<PhotoSettings> Photos { get; set; }

        public bool ShufflePhotos { get; set; }
    }
}