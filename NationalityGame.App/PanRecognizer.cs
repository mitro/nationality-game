using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NationalityGame.App
{
    public class PanRecognizer
    {
        private readonly Canvas _canvas;

        private Point _startPosition;

        public event Action<Vector> PanRecognized;

        public PanRecognizer(Canvas canvas)
        {
            _canvas = canvas;

            _canvas.MouseUp += CanvasOnMouseUp;
            _canvas.MouseDown += CanvasOnMouseDown;
        }

        private void CanvasOnMouseDown(object sender, MouseButtonEventArgs args)
        {
            _startPosition = args.GetPosition(_canvas);
        }

        private void CanvasOnMouseUp(object sender, MouseButtonEventArgs args)
        {
            var endPosition = args.GetPosition(_canvas);

            var vector1 = new Vector(
                endPosition.X - _startPosition.X,
                endPosition.Y - _startPosition.Y);

            if (vector1.Length >= 10)
            {
                PanRecognized?.Invoke(vector1);
            }
        }
    }
}