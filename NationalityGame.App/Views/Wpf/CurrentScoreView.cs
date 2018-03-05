using System.Windows;
using System.Windows.Controls;
using NationalityGame.App.Utils;
using NationalityGame.Presentation.Views;

namespace NationalityGame.App.Views.Wpf
{
    public class CurrentScoreView : ICurrentScoreView
    {
        private readonly Canvas _canvas;

        private Label _scoreLabel;

        public CurrentScoreView(Canvas canvas)
        {
            _canvas = canvas;

            CreateControls();
        }

        public void Show(int currentScore)
        {
            UiThread.Dispatch(() =>
            {
                _scoreLabel.Content = $"Current score is {currentScore}";
                _scoreLabel.Visibility = Visibility.Visible;
            });
        }

        public void Hide()
        {
            UiThread.Dispatch(() =>
            {
                _scoreLabel.Visibility = Visibility.Hidden;
            });
        }

        private void CreateControls()
        {
            UiThread.Dispatch(CreateScoreLabel);
        }

        private void CreateScoreLabel()
        {
            _scoreLabel = new Label
            {
                FontSize = 30,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Width = 400,
                Visibility = Visibility.Hidden,
            };

            Canvas.SetLeft(_scoreLabel, (_canvas.ActualWidth - _scoreLabel.Width) / 2);
            Canvas.SetTop(_scoreLabel, _canvas.ActualHeight - 80);

            Panel.SetZIndex(_scoreLabel, 10000);

            _canvas.Children.Add(_scoreLabel);
        }
    }
}