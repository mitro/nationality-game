using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NationalityGame.Mechanics;

namespace NationalityGame.App
{
    public class PanRecognizer
    {
        private readonly Canvas _canvas;

        private Point _startPosition;

        // TODO Init
        public event Action<Vector> PanRecognized;

        public PanRecognizer(Canvas canvas)
        {
            _canvas = canvas;

            _canvas.MouseDown += CanvasOnMouseDown;

            _canvas.MouseUp += CanvasOnMouseUp;
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

            // TODO Get from settings
            if (vector1.Length >= 20)
            {
                PanRecognized?.Invoke(vector1);
            }
        }
    }
}