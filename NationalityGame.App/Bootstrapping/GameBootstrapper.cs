using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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

            var buckets = new List<Bucket>
            {
                new Bucket("Japaneese", new Point(0, 0), 150, 150),
                new Bucket("Chinese", new Point(board.Width - 150, 0), 150, 150),
                new Bucket("Korean", new Point(board.Width - 150, board.Height - 150), 150, 150),
                new Bucket("Thai", new Point(0, board.Height - 150), 150, 150)
            };

            var settingsReader = new SettingsReader();

            var settings = settingsReader.Read();

            var photos = settings.Photos
                .Select(photo => new Photo(
                    new Point(board.Width / 2, 0),
                    photo.Nationality,
                    photo.ImagePath
                    ));

            var velocity = board.Height / 3000;

            var gameSettings = new Game.Settings(settings.ShufflePhotos, velocity);

            var score = new Score(20, -5);

            _game = new Game(gameSettings, board, buckets, photos, score);

            var presenterFactory = new GamePresenterWpfFactory();

            _gamePresenter = presenterFactory.Create(_game, window);

            _gamePresenter.Start();
        }
    }
}