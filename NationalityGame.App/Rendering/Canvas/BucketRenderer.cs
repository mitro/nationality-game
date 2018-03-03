using System;
using System.Windows.Media;
using System.Windows.Shapes;
using NationalityGame.Mechanics;

namespace NationalityGame.App.Rendering.Canvas
{
    class BucketRenderer
    {
        private readonly Bucket _bucket;

        private readonly System.Windows.Controls.Canvas _canvas;

        private Rectangle _rectangle;

        public BucketRenderer(Game game, Bucket bucket, System.Windows.Controls.Canvas canvas)
        {
            _bucket = bucket;
            _canvas = canvas;

            _rectangle = new Rectangle
            {
                Height = 100,
                Width = 100,
                Fill = new SolidColorBrush(Colors.Blue),
            };

            System.Windows.Controls.Canvas.SetTop(_rectangle, bucket.Center.Y);
            System.Windows.Controls.Canvas.SetLeft(_rectangle, bucket.Center.X);

            _canvas.Children.Add(_rectangle);
        }

        public void Render()
        {

        }
    }
}