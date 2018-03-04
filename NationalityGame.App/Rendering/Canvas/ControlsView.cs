﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace NationalityGame.App.Rendering.Canvas
{
    public class ControlsView : IView, IControlsView
    {
        private readonly System.Windows.Controls.Canvas _canvas;
        private readonly Label _label;
        private readonly StackPanel _stackPanel;

        public ControlsView(System.Windows.Controls.Canvas canvas)
        {
            _canvas = canvas;

            _stackPanel = new StackPanel
            {
                Width = 400,
                Height = 200,
                Visibility = Visibility.Hidden,
            };

            System.Windows.Controls.Canvas.SetLeft(_stackPanel, (_canvas.ActualWidth - _stackPanel.Width) / 2);
            System.Windows.Controls.Canvas.SetTop(_stackPanel, (_canvas.ActualHeight - _stackPanel.Height) / 2);

            _canvas.Children.Add(_stackPanel);

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
            NewGameRequested?.Invoke();
        }

        public void Render()
        {
            
        }

        public event Action NewGameRequested;

        public void Show(int totalScore)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _label.Content = $"You total score is {totalScore}";

                _stackPanel.Visibility = Visibility.Visible;
            });
        }

        public void Hide()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _label.Content = string.Empty;

                _stackPanel.Visibility = Visibility.Hidden;
            });
        }
    }
}