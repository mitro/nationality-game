using System.ComponentModel;
using System.Timers;
using System.Windows;
using NationalityGame.App.Rendering;
using NationalityGame.App.Rendering.Canvas;
using NationalityGame.Mechanics;

namespace NationalityGame.App
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Game _game;

        private CanvasPresenter _canvasRenderer;
        private PanRecognizer _panRecognizer;

        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();

        public GameWindow()
        {
            InitializeComponent();

            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;
        }

        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            Dispatcher.Invoke(() => _canvasRenderer.Render());
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            _game.Tick();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var settingsReader = new SettingsReader();

            var settings = settingsReader.Read();

            _panRecognizer = new PanRecognizer(GameCanvas);

            _panRecognizer.PanRecognized += PanRecognizerOnPanRecognized;

            _game = new Game(GameCanvas.ActualWidth, GameCanvas.ActualHeight, settings);

            _canvasRenderer = new CanvasPresenter(this, _game);

            Timer timer = new Timer
            {
                Interval = 30
            };

            timer.Elapsed += TimerOnElapsed;

            _game.Start();

            timer.Start();
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
