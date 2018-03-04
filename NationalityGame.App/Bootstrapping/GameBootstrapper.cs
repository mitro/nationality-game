using System.Collections.Generic;
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

            _game = new Game(board, buckets);

            var presenterFactory = new GamePresenterWpfFactory();

            _gamePresenter = presenterFactory.Create(_game, window);

            _gamePresenter.Start();
        }
    }
}