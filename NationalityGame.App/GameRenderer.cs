using System;
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

        public GameRenderer(Canvas canvas, Game game)
        {
            _canvas = canvas;
            _game = game;

            _canvas.Children.Clear();
        }

        public void Render()
        {
            _canvas.Children.Clear();

            RenderBucket(_game.TopLeftBucket);
            RenderBucket(_game.TopRightBucket);
            RenderBucket(_game.BottomRightBucket);
            RenderBucket(_game.BottomLeftBucket);

            RenderPhoto();
        }

        private void RenderBucket(Bucket bucket)
        {
            var rectangle = new Rectangle
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

            SetCornerPosition(rectangle, bucket.Corner);

            _canvas.Children.Add(rectangle);
        }

        private void RenderPhoto()
        {
            if (_game.CurrentPhoto == null)
            {
                return;
            }

            var rectangle = new Rectangle
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

            Canvas.SetLeft(rectangle, _game.CurrentPhoto.Center.X);
            Canvas.SetTop(rectangle, _game.CurrentPhoto.Center.Y);

            _canvas.Children.Add(rectangle);
        }

        private void SetCornerPosition(Rectangle rectangle, Corner corner)
        {
            switch (corner)
            {
                case Corner.TopLeft:
                    Canvas.SetTop(rectangle, 0);
                    Canvas.SetLeft(rectangle, 0);
                    break;

                case Corner.TopRight:
                    Canvas.SetTop(rectangle, 0);
                    Canvas.SetRight(rectangle, 0);
                    break;

                case Corner.BottomRight:
                    Canvas.SetBottom(rectangle, 0);
                    Canvas.SetRight(rectangle, 0);
                    break;

                case Corner.BottomLeft:
                    Canvas.SetBottom(rectangle, 0);
                    Canvas.SetLeft(rectangle, 0);
                    break;

                default:
                    throw new ArgumentException($"Invalid {nameof(Corner)} value: {corner}. Unable to proceed. Check the code.");
            }
        }
    }
}