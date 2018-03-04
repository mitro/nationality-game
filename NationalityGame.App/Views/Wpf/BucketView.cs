using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using NationalityGame.Mechanics;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Presentation.Views;

namespace NationalityGame.App.Rendering.Canvas
{
    class BucketView : IBucketView
    {
        private readonly Rectangle _rectangle;
        private readonly Label _label;

        public BucketView(Bucket bucket, System.Windows.Controls.Canvas canvas)
        {
            _rectangle = new Rectangle
            {
                Height = bucket.Height,
                Width = bucket.Width,
                Fill = new SolidColorBrush(Colors.BlanchedAlmond),
                Visibility = Visibility.Hidden,
            };

            _label = new Label
            {
                Content = bucket.Nationality,
                Height = bucket.Height,
                Width = bucket.Width,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Visibility = Visibility.Hidden,
                FontSize = 16
            };

            System.Windows.Controls.Canvas.SetTop(_rectangle, bucket.Position.Y);
            System.Windows.Controls.Canvas.SetLeft(_rectangle, bucket.Position.X);

            System.Windows.Controls.Canvas.SetTop(_label, bucket.Position.Y);
            System.Windows.Controls.Canvas.SetLeft(_label, bucket.Position.X);

            canvas.Children.Add(_rectangle);
            canvas.Children.Add(_label);
        }

        public void Show()
        {
            _rectangle.Visibility = Visibility.Visible;
            _label.Visibility = Visibility.Visible;
        }
    }
}