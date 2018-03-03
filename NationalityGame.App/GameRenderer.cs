using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using NationalityGame.Mechanics;

namespace NationalityGame.App
{
    public class GameRenderer
    {
        private readonly Canvas _canvas;

        private readonly Game _game;

        private List<BucketRenderer> _bucketRenderers = new List<BucketRenderer>();

        private PhotoRenderer _photoRenderer;

        public GameRenderer(Canvas canvas, Game game)
        {
            _canvas = canvas;
            _game = game;

            AddGameObjects();
        }

        private void AddGameObjects()
        {
            _canvas.Children.Clear();

            _bucketRenderers = _game.Buckets
                .Select(bucket => new BucketRenderer(bucket, _canvas))
                .ToList();

            _photoRenderer = new PhotoRenderer(_game.CurrentPhoto, _canvas);
        }

        public void Render()
        {
            _bucketRenderers.ForEach(bucket => bucket.Render());
            _photoRenderer.Render();
        }

        class BucketRenderer
        {
            private readonly Bucket _bucket;

            private readonly Canvas _canvas;

            private Rectangle _rectangle;

            public BucketRenderer(Bucket bucket, Canvas canvas)
            {
                _bucket = bucket;
                _canvas = canvas;

                _rectangle = new Rectangle
                {
                    Height = 100,
                    Width = 100,
                    Fill = new SolidColorBrush(Colors.Blue),
                    //BorderBrush = new SolidColorBrush(Colors.Black),
                    //BorderThickness = new Thickness(2),
                    //Child = new TextBlock
                    //{
                    //    Text = bucket.Label,
                    //    HorizontalAlignment = HorizontalAlignment.Center,
                    //    VerticalAlignment = VerticalAlignment.Center,
                    //},
                };

                switch (_bucket.Corner)
                {
                    case Corner.TopLeft:
                        Canvas.SetTop(_rectangle, 0);
                        Canvas.SetLeft(_rectangle, 0);
                        break;

                    case Corner.TopRight:
                        Canvas.SetTop(_rectangle, 0);
                        Canvas.SetRight(_rectangle, 0);
                        break;

                    case Corner.BottomRight:
                        Canvas.SetBottom(_rectangle, 0);
                        Canvas.SetRight(_rectangle, 0);
                        break;

                    case Corner.BottomLeft:
                        Canvas.SetBottom(_rectangle, 0);
                        Canvas.SetLeft(_rectangle, 0);
                        break;

                    default:
                        throw new ArgumentException($"Invalid {nameof(Corner)} value: {_bucket.Corner}. Unable to proceed. Check the code.");
                }

                _canvas.Children.Add(_rectangle);
            }

            public void Render()
            {

            }
        }

        public class PhotoRenderer
        {
            private readonly Photo _photo;

            private readonly Canvas _canvas;

            private Rectangle _rectangle;

            public PhotoRenderer(Photo photo, Canvas canvas)
            {
                _photo = photo;
                _canvas = canvas;

                _rectangle = new Rectangle
                {
                    Height = 100,
                    Width = 100,
                    Fill = new SolidColorBrush(Colors.Blue),
                    //BorderBrush = new SolidColorBrush(Colors.Black),
                    //BorderThickness = new Thickness(2),
                    //Child = new TextBlock
                    //{
                    //    Text = bucket.Label,
                    //    HorizontalAlignment = HorizontalAlignment.Center,
                    //    VerticalAlignment = VerticalAlignment.Center,
                    //},
                };

                _canvas.Children.Add(_rectangle);
            }

            public void Render()
            {
                Canvas.SetLeft(_rectangle, _photo.Center.X);
                Canvas.SetTop(_rectangle, _photo.Center.Y);
            }
        }
    }
}