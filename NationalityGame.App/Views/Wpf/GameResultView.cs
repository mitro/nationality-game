using System;
using System.Windows;
using System.Windows.Controls;
using NationalityGame.App.Utils;
using NationalityGame.Presentation.Views;

namespace NationalityGame.App.Views.Wpf
{
    public class GameResultView : IGameResultView
    {
        private readonly Label _label;
        private readonly StackPanel _stackPanel;

        public GameResultView(Canvas canvas)
        {
            _stackPanel = new StackPanel
            {
                Width = 400,
                Height = 200,
                Visibility = Visibility.Hidden,
            };

            Canvas.SetLeft(_stackPanel, (canvas.ActualWidth - _stackPanel.Width) / 2);
            Canvas.SetTop(_stackPanel, (canvas.ActualHeight - _stackPanel.Height) / 2);

            canvas.Children.Add(_stackPanel);

            _label = new Label
            {
                FontSize = 36,
                Margin = new Thickness(0, 0, 0, 10),
                HorizontalContentAlignment = HorizontalAlignment.Center,
            };

            _stackPanel.Children.Add(_label);

            var button = new Button
            {
                Content = "Play again",
                Width = 200,
                Height = 50,
            };

            button.Click += ButtonOnClick;

            _stackPanel.Children.Add(button);
        }

        private void ButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            PlayAgainExecuted?.Invoke();
        }

        public event Action PlayAgainExecuted;

        public void Show(int totalScore)
        {
            UiThread.Dispatch(() =>
            {
                _label.Content = $"Your total score is {totalScore}";

                _stackPanel.Visibility = Visibility.Visible;
            });
        }

        public void Hide()
        {
            UiThread.Dispatch(() =>
            {
                _label.Content = string.Empty;

                _stackPanel.Visibility = Visibility.Hidden;
            });
        }
    }
}