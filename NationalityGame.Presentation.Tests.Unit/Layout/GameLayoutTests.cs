using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NationalityGame.Configuration.Settings;
using NationalityGame.Configuration.Settings.Sections;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Presentation.Layout;
using Xunit;

namespace NationalityGame.Presentation.Tests.Unit.Layout
{
    public class GameLayoutTests
    {
        [Fact]
        public void Should_create_buckets()
        {
            var settings = new GameSettings
            {
                Buckets = new BucketsSettings
                {
                    TopLeft = new BucketSettings { Nationality = "Thai" },
                    TopRight = new BucketSettings { Nationality = "Chinese" },
                    BottomRight = new BucketSettings { Nationality = "Korean" },
                    BottomLeft = new BucketSettings { Nationality = "Japanese" },
                },
                Appearance = new AppearanceSettings
                {
                    BucketWidth = 100,
                    BucketHeight = 200,
                },
            };

            var board = new Board(1000, 500);

            var layout = new GameLayout();

            var buckets = layout.CreateBuckets(settings, board);

            ShouldHaveBucket(buckets, "Thai", 50, 100);
            ShouldHaveBucket(buckets, "Chinese", 950, 100);
            ShouldHaveBucket(buckets, "Korean", 950, 400);
            ShouldHaveBucket(buckets, "Japanese", 50, 400);
        }

        [Fact]
        public void Should_create_photos()
        {
            var settings = new GameSettings
            {
                Photos = new List<PhotoSettings>
                {
                    new PhotoSettings { ImagePath = "Path1", Nationality = "Thai" },
                    new PhotoSettings { ImagePath = "Path2", Nationality = "Korean" },
                }
            };

            var board = new Board(1000, 500);

            var layout = new GameLayout();

            var photos = layout.CreatePhotos(settings, board);

            ShouldHavePhoto(photos, "Path1", "Thai", 500, 0);
            ShouldHavePhoto(photos, "Path2", "Korean", 500, 0);
        }

        private void ShouldHaveBucket(IEnumerable<Bucket> buckets, string nationality, double centerX, double centerY)
        {
            buckets.Count(b => b.Nationality == nationality &&
                               b.Center.X == centerX &&
                               b.Center.Y == centerY)
                .Should().Be(1);
        }

        private void ShouldHavePhoto(IEnumerable<Photo> photos, string path, string nationality, double centerX,
            double centerY)
        {
            photos.Count(p => p.ImagePath == path &&
                              p.Nationality == nationality &&
                              p.Center.X == centerX &&
                              p.Center.Y == centerY)
                .Should().Be(1);
        }
    }
}