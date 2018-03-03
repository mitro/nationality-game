using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using NationalityGame.Mechanics;

namespace NationalityGame.App.Rendering.Canvas
{
    class BucketRenderer
    {
        public BucketRenderer(Bucket bucket, System.Windows.Controls.Canvas canvas)
        {
            var rectangle = new Rectangle
            {
                Height = bucket.Height,
                Width = bucket.Width,
                Fill = new SolidColorBrush(Colors.BlanchedAlmond)
            };

            var textBlock = new Label
            {
                Content = bucket.Nationality,
                Height = bucket.Height,
                Width = bucket.Width,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 16
            };

            System.Windows.Controls.Canvas.SetTop(rectangle, bucket.Position.Y);
            System.Windows.Controls.Canvas.SetLeft(rectangle, bucket.Position.X);

            System.Windows.Controls.Canvas.SetTop(textBlock, bucket.Position.Y);
            System.Windows.Controls.Canvas.SetLeft(textBlock, bucket.Position.X);

            canvas.Children.Add(rectangle);
            canvas.Children.Add(textBlock);
        }

        public void Render()
        {

        }
    }
}