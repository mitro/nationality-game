using System.ComponentModel;
using System.Timers;
using System.Windows;
using NationalityGame.Mechanics;
using NationalityGame.Presentation;

namespace NationalityGame.App.Bootstrapping
{
    public class GameBootstrapper
    {
        private Game _game;

        private GamePresenter _gamePresenter;
        private BackgroundWorker _backgroundWorker;

        public void Bootstrap(GameWindow window)
        {
            _game = new Game(window.GameCanvas.ActualWidth, window.GameCanvas.ActualHeight);

            var presenterFactory = new GamePresenterWpfFactory();

            _gamePresenter = presenterFactory.Create(_game, window);

            _gamePresenter.Start();
        }
    }
}