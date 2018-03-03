using System;
using System.Windows;
using System.Windows.Threading;
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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            _game.Tick();
            _gameRenderer.Render();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _panRecognizer = new PanRecognizer(GameCanvas);

            _panRecognizer.PanRecognized += PanRecognizerOnPanRecognized;

            var gameBuilder = new GameBuilder();

            _game = gameBuilder.Build(GameCanvas.ActualWidth, GameCanvas.ActualHeight);

            _gameRenderer = new GameRenderer(GameCanvas, _game);

            _gameRenderer.Render();

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(10),
            };

            timer.Tick += TimerOnTick;

            timer.Start();
        }

        private void PanRecognizerOnPanRecognized(Vector vector)
        {
            _game.ProcessPan(vector);
        }
    }
}
