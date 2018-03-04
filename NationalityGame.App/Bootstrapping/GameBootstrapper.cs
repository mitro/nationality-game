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

            _game = new Game(board);

            var presenterFactory = new GamePresenterWpfFactory();

            _gamePresenter = presenterFactory.Create(_game, window);

            _gamePresenter.Start();
        }
    }
}