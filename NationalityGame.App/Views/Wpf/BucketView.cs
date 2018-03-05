using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Presentation.Views;

namespace NationalityGame.App.Views.Wpf
{
    class BucketView : IBucketView
    {
        private readonly Rectangle _rectangle;

        private readonly Label _label;

        public BucketView(Bucket bucket, Canvas canvas, double width, double height)
        {
            _rectangle = new Rectangle
            {
                Height = height,
                Width = width,
                Fill = new SolidColorBrush(Colors.BlanchedAlmond),
                Visibility = Visibility.Hidden,
            };

            _label = new Label
            {
                Content = bucket.Nationality,
                Height = height,
                Width = width,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Visibility = Visibility.Hidden,
                FontSize = 16
            };

            Canvas.SetTop(_rectangle, bucket.Position.Y - height / 2);
            Canvas.SetLeft(_rectangle, bucket.Position.X - width / 2);

            Canvas.SetTop(_label, bucket.Position.Y - height / 2);
            Canvas.SetLeft(_label, bucket.Position.X - width / 2);

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