using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NationalityGame.Configuration;
using NationalityGame.Mechanics;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Presentation;

namespace NationalityGame.App.Bootstrapping
{
    public class GameBootstrapper
    {
        private Game _game;

        private GamePresenter _gamePresenter;

        public void Bootstrap(GameWindow window)
        {
            var board = new Board(window.GameCanvas.ActualWidth, window.GameCanvas.ActualHeight);

            var settingsReader = new SettingsReader();

            var settings = settingsReader.Read();

            var bucketHalfWidth = settings.Appearance.BucketWidth / 2;
            var bucketHalfHeight = settings.Appearance.BucketHeight / 2;

            var buckets = new List<Bucket>
            {
                new Bucket(settings.Buckets.TopLeft.Nationality, new Point(bucketHalfWidth, bucketHalfHeight)),
                new Bucket(settings.Buckets.TopRight.Nationality, new Point(board.Width - bucketHalfWidth, bucketHalfHeight)),
                new Bucket(settings.Buckets.BottomRight.Nationality, new Point(board.Width - bucketHalfWidth, board.Height - bucketHalfHeight)),
                new Bucket(settings.Buckets.BottomLeft.Nationality, new Point(bucketHalfWidth, board.Height - bucketHalfHeight))
            };

            var photos = settings.Photos
                .Select(photo => new Photo(
                    new Point(board.Width / 2, 0),
                    photo.Nationality,
                    photo.ImagePath
                    ));

            var velocity = board.Height / settings.Appearance.PhotoRunTimeInMs;

            var gameSettings = new Game.Settings(settings.Rules.ShufflePhotos, velocity);

            var score = new ScoringStrategy(settings.Rules.CorrectChoiceScore, settings.Rules.IncorrectChoiceScore);

            _game = new Game(gameSettings, board, buckets, photos, score);

            var presenterFactory = new GamePresenterWpfFactory();

            _gamePresenter = presenterFactory.Create(_game, settings, window);

            _gamePresenter.Start();
        }
    }
}