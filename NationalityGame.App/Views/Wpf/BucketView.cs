using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using NationalityGame.App.Utils;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Presentation.Views;

namespace NationalityGame.App.Views.Wpf
{
    class BucketView : IBucketView
    {
        private readonly Bucket _bucket;

        private readonly Canvas _canvas;

        private readonly double _width;

        private readonly double _height;

        private Rectangle _rectangle;

        private Label _nationalityLabel;

        public BucketView(Bucket bucket, Canvas canvas, double width, double height)
        {
            _bucket = bucket;
            _canvas = canvas;
            _width = width;
            _height = height;

            AddControls();
        }

        public void Show()
        {
            UiThread.Dispatch(() =>
            {
                _rectangle.Visibility = Visibility.Visible;
                _nationalityLabel.Visibility = Visibility.Visible;
            });
        }

        private void AddControls()
        {
            UiThread.Dispatch(() =>
            {
                AddRectangle();

                AddNationalityLabel();
            });
        }

        private void AddRectangle()
        {
            _rectangle = new Rectangle
            {
                Height = _height,
                Width = _width,
                Fill = new SolidColorBrush(Colors.BlanchedAlmond),
                Visibility = Visibility.Hidden,
            };

            Canvas.SetTop(_rectangle, _bucket.Center.Y - _height / 2);
            Canvas.SetLeft(_rectangle, _bucket.Center.X - _width / 2);

            _canvas.Children.Add(_rectangle);
        }

        private void AddNationalityLabel()
        {
            _nationalityLabel = new Label
            {
                Content = _bucket.Nationality,
                Height = _height,
                Width = _width,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Visibility = Visibility.Hidden,
                FontSize = 20
            };

            Canvas.SetTop(_nationalityLabel, _bucket.Center.Y - _height / 2);
            Canvas.SetLeft(_nationalityLabel, _bucket.Center.X - _width / 2);

            _canvas.Children.Add(_nationalityLabel);
        }
    }
}