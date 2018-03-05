using System;
using System.Windows;
using System.Windows.Controls;
using NationalityGame.App.Utils;
using NationalityGame.Presentation.Views;

namespace NationalityGame.App.Views.Wpf
{
    public class GameResultView : IGameResultView
    {
        private readonly Canvas _canvas;

        private Label _scoreLabel;

        private StackPanel _stackPanel;

        public GameResultView(Canvas canvas)
        {
            _canvas = canvas;

            CreateControls();
        }

        public event Action PlayAgainRequested;

        public void Show(int totalScore)
        {
            UiThread.Dispatch(() =>
            {
                _scoreLabel.Content = $"Your total score is {totalScore}";

                _stackPanel.Visibility = Visibility.Visible;
            });
        }

        public void Hide()
        {
            UiThread.Dispatch(() =>
            {
                _scoreLabel.Content = string.Empty;

                _stackPanel.Visibility = Visibility.Hidden;
            });
        }

        private void CreateControls()
        {
            UiThread.Dispatch(() =>
            {
                CreateStackPanel();

                CreateScoreLabel();

                CreatePlayAgainButton();
            });
        }

        private void CreateStackPanel()
        {
            _stackPanel = new StackPanel
            {
                Width = 400,
                Height = 200,
                Visibility = Visibility.Hidden,
            };

            Canvas.SetLeft(_stackPanel, (_canvas.ActualWidth - _stackPanel.Width) / 2);
            Canvas.SetTop(_stackPanel, (_canvas.ActualHeight - _stackPanel.Height) / 2);

            _canvas.Children.Add(_stackPanel);
        }

        private void CreateScoreLabel()
        {
            _scoreLabel = new Label
            {
                FontSize = 36,
                Margin = new Thickness(0, 0, 0, 10),
                HorizontalContentAlignment = HorizontalAlignment.Center,
            };

            _stackPanel.Children.Add(_scoreLabel);
        }

        private void CreatePlayAgainButton()
        {
            var playAgainButton = new Button
            {
                Content = "Play again",
                Width = 200,
                Height = 50,
            };

            playAgainButton.Click += PlayAgainButtonOnClick;

            _stackPanel.Children.Add(playAgainButton);
        }

        private void PlayAgainButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            PlayAgainRequested?.Invoke();
        }
    }
}