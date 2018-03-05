using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NationalityGame.Configuration.Settings;
using NationalityGame.Mechanics.Domain;

namespace NationalityGame.Presentation.Layout
{
    public class GameLayout : IGameLayout
    {
        public List<Bucket> CreateBuckets(GameSettings settings, Board board)
        {
            var bucketHalfWidth = settings.Appearance.BucketWidth / 2;
            var bucketHalfHeight = settings.Appearance.BucketHeight / 2;

            return new List<Bucket>
            {
                new Bucket(
                    settings.Buckets.TopLeft.Nationality,
                    new Point(bucketHalfWidth, bucketHalfHeight)),

                new Bucket(
                    settings.Buckets.TopRight.Nationality,
                    new Point(board.Width - bucketHalfWidth, bucketHalfHeight)),

                new Bucket(
                    settings.Buckets.BottomRight.Nationality,
                    new Point(board.Width - bucketHalfWidth, board.Height - bucketHalfHeight)),

                new Bucket(
                    settings.Buckets.BottomLeft.Nationality,
                    new Point(bucketHalfWidth, board.Height - bucketHalfHeight))
            };
        }

        public IEnumerable<Photo> CreatePhotos(GameSettings settings, Board board)
        {
            return settings.Photos
                .Select(photo => new Photo(
                    new Point(board.Width / 2, 0),
                    photo.Nationality,
                    photo.ImagePath
                ));
        }
    }
}