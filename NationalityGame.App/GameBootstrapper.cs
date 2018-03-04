using System.ComponentModel;
using System.Timers;
using System.Windows;
using NationalityGame.App.Rendering.Canvas;
using NationalityGame.Mechanics;

namespace NationalityGame.App
{
    public class GameBootstrapper
    {
        private PanRecognizer _panRecognizer;

        private Game _game;

        private GamePresenter _gamePresenter;
        private BackgroundWorker _backgroundWorker;

        public void Bootstrap(GameWindow window)
        {
            _backgroundWorker = new BackgroundWorker();

            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;

            _panRecognizer = new PanRecognizer(window.GameCanvas);

            _panRecognizer.PanRecognized += PanRecognizerOnPanRecognized;

            _game = new Game(window.GameCanvas.ActualWidth, window.GameCanvas.ActualHeight);

            var presenterFactory = new GamePresenterWpfFactory();

            _gamePresenter = presenterFactory.Create(_game, window);
            _gamePresenter.Prepare();

            Timer timer = new Timer
            {
                Interval = 30
            };

            timer.Elapsed += TimerOnElapsed;

            _game.Start();

            timer.Start();
        }

        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            //Dispatcher.Invoke(() => _canvasRenderer.Render());
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

        private void PanRecognizerOnPanRecognized(Vector vector)
        {
            _game.ProcessPan(vector);
        }
    }
}