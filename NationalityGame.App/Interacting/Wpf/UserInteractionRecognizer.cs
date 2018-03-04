using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NationalityGame.Presentation.Interacting;

namespace NationalityGame.App.Interacting.Wpf
{
    public class UserInteractionRecognizer : IUserInteractionRecognizer
    {
        private readonly Canvas _canvas;

        private Point _startPosition;

        public event Action<Vector> PanRecognized;

        public UserInteractionRecognizer(Canvas canvas)
        {
            _canvas = canvas;

            _canvas.MouseUp += CanvasOnMouseUp;
            _canvas.MouseDown += CanvasOnMouseDown;
        }

        private void CanvasOnMouseDown(object sender, MouseButtonEventArgs args)
        {
            _startPosition = args.GetPosition(_canvas);
        }

        // TODO Check pan is over image
        private void CanvasOnMouseUp(object sender, MouseButtonEventArgs args)
        {
            var endPosition = args.GetPosition(_canvas);

            var vector1 = new Vector(
                endPosition.X - _startPosition.X,
                endPosition.Y - _startPosition.Y);

            // TODO Move to settings
            if (vector1.Length >= 20)
            {
                PanRecognized?.Invoke(vector1);
            }
        }
    }
}