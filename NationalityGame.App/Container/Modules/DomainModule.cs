using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Autofac;
using NationalityGame.Configuration.Settings;
using NationalityGame.Mechanics.Domain;

namespace NationalityGame.App.Container.Modules
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
                {
                    var window = c.Resolve<GameWindow>();

                    var settings = c.Resolve<GameSettings>();

                    var board = CreateBoard(window);

                    var buckets = CreateBuckets(settings, board);

                    var photos = CreatePhotos(settings, board);

                    return new Game(board, buckets, photos);
                })
                .SingleInstance();
        }

        private static Board CreateBoard(GameWindow window)
        {
            return new Board(
                window.GameCanvas.ActualWidth,
                window.GameCanvas.ActualHeight);
        }

        private static List<Bucket> CreateBuckets(GameSettings settings, Board board)
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

        private static IEnumerable<Photo> CreatePhotos(GameSettings settings, Board board)
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