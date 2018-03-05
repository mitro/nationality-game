using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using NationalityGame.App.Views.Wpf;
using NationalityGame.Presentation.Interactivity;

namespace NationalityGame.App.Interactivity.Wpf
{
    public class UserInteractionRecognizer : IUserInteractionRecognizer
    {
        private readonly Canvas _canvas;

        private Point _startPosition;

        private bool _gestureInProgress;

        public UserInteractionRecognizer(Canvas canvas)
        {
            _canvas = canvas;

            _canvas.MouseDown += CanvasOnMouseDown;
            _canvas.MouseUp += CanvasOnMouseUp;
        }

        public event Action<Vector> PanRecognized;

        private void CanvasOnMouseDown(object sender, MouseButtonEventArgs args)
        {
            if (args.Source is Shape sourceShape &&
                sourceShape.Name == PhotoView.PhotoShapeName)
            {
                _gestureInProgress = true;
                _startPosition = args.GetPosition(_canvas);
            }
        }

        private void CanvasOnMouseUp(object sender, MouseButtonEventArgs args)
        {
            if (!_gestureInProgress)
            {
                return;
            }

            _gestureInProgress = false;

            var endPosition = args.GetPosition(_canvas);

            var vector = new Vector(
                endPosition.X - _startPosition.X,
                endPosition.Y - _startPosition.Y);

            if (vector.Length >= 20)
            {
                PanRecognized?.Invoke(vector);
            }
        }
    }
}