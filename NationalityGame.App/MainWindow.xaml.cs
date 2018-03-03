using System.ComponentModel;
using System.Timers;
using System.Windows;
using NationalityGame.Mechanics;

namespace NationalityGame.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game _game;

        private GameRenderer _gameRenderer;
        private PanRecognizer _panRecognizer;

        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();

            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;
        }

        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            Dispatcher.Invoke(() => _gameRenderer.Render());
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            _game.Tick();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _panRecognizer = new PanRecognizer(GameCanvas);

            _panRecognizer.PanRecognized += PanRecognizerOnPanRecognized;

            var gameBuilder = new GameBuilder();

            _game = gameBuilder.Build(GameCanvas.ActualWidth, GameCanvas.ActualHeight);

            _gameRenderer = new GameRenderer(GameCanvas, _game);

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
