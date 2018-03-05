using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Autofac;
using NationalityGame.App.Interactivity.Wpf;
using NationalityGame.App.Views.Wpf;
using NationalityGame.Configuration.Settings;
using NationalityGame.Mechanics;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Presentation;
using NationalityGame.Presentation.Interactivity;
using NationalityGame.Presentation.Views;

namespace NationalityGame.App.Container
{
    public static class AutofacContainer
    {
        public static IContainer Build(GameWindow gameWindow, GameSettings gameSettings)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(gameWindow);

            builder.RegisterInstance(gameSettings);

            builder.Register(c =>
                {
                    var settings = c.Resolve<GameSettings>();

                    return new ScoringStrategy(settings.Rules.CorrectChoiceScore, settings.Rules.IncorrectChoiceScore);
                })
                .As<IScoringStrategy>();

            builder.RegisterType<GameEngine>()
                .As<IGameEngine>();

            builder.Register(c =>
            {
                var game = c.Resolve<Game>();
                var settings = c.Resolve<GameSettings>();

                var velocity = game.Board.Height / settings.Appearance.PhotoRunTimeInMs;

                return new GameEngine.Settings(settings.Rules.ShufflePhotos, velocity);
            });

            builder.RegisterType<GamePresenter>()
                .As<IGamePresenter>();

            builder.Register(c =>
            {
                var window = c.Resolve<GameWindow>();

                return new UserInteractionRecognizer(window.GameCanvas);
            })
                .As<IUserInteractionRecognizer>();

            builder.Register(c =>
            {
                var settings = c.Resolve<GameSettings>();

                return new Ticker(settings.Appearance.TickIntervalInMs);
            })
                .As<ITicker>();

            builder.Register(c =>
            {
                var window = c.Resolve<GameWindow>();

                var settings = c.Resolve<GameSettings>();

                var board = new Board(window.GameCanvas.ActualWidth, window.GameCanvas.ActualHeight);

                var bucketHalfWidth = settings.Appearance.BucketWidth / 2;
                var bucketHalfHeight = settings.Appearance.BucketHeight / 2;

                var buckets = new List<Bucket>
                {
                    new Bucket(settings.Buckets.TopLeft.Nationality, new Point(bucketHalfWidth, bucketHalfHeight)),
                    new Bucket(settings.Buckets.TopRight.Nationality,
                        new Point(board.Width - bucketHalfWidth, bucketHalfHeight)),
                    new Bucket(settings.Buckets.BottomRight.Nationality,
                        new Point(board.Width - bucketHalfWidth, board.Height - bucketHalfHeight)),
                    new Bucket(settings.Buckets.BottomLeft.Nationality,
                        new Point(bucketHalfWidth, board.Height - bucketHalfHeight))
                };

                var photos = settings.Photos
                    .Select(photo => new Photo(
                        new Point(board.Width / 2, 0),
                        photo.Nationality,
                        photo.ImagePath
                    ));

                return new Game(board, buckets, photos);
            })
                .SingleInstance();

            builder.Register(c =>
            {
                var window = c.Resolve<GameWindow>();
                var settings = c.Resolve<GameSettings>();

                return new PhotoView(window.GameCanvas, settings.Appearance.PhotoWidth,
                    settings.Appearance.PhotoHeight);
            })
                .As<IPhotoView>();

            builder.Register(c =>
            {
                var window = c.Resolve<GameWindow>();

                return new GameResultView(window.GameCanvas);
            })
                .As<IGameResultView>();

            builder.Register(c =>
            {
                var window = c.Resolve<GameWindow>();
                var settings = c.Resolve<GameSettings>();
                var game = c.Resolve<Game>();

                return game.Buckets
                    .Select(bucket => new BucketView(
                        bucket,
                        window.GameCanvas,
                        settings.Appearance.BucketWidth,
                        settings.Appearance.BucketHeight))
                    .ToList();
            })
                .As<IEnumerable<IBucketView>>();

            return builder.Build();
        }
    }
}