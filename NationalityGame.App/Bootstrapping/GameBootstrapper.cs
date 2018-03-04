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
            _backgroundWorker = new BackgroundWorker();

            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;

            _game = new Game(window.GameCanvas.ActualWidth, window.GameCanvas.ActualHeight);

            var presenterFactory = new GamePresenterWpfFactory();

            _gamePresenter = presenterFactory.Create(_game, window);
            _gamePresenter.Start();

            Timer timer = new Timer
            {
                Interval = 30
            };

            timer.Elapsed += TimerOnElapsed;

            _game.Start();

            timer.Start();
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            _game.Tick();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs args)
        {
            if (_backgroundWorker.IsBusy)
            {
                return;
            }

            _backgroundWorker.RunWorkerAsync();
        }
    }
}