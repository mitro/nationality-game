using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NationalityGame.App.Utils;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Presentation.Views;

namespace NationalityGame.App.Views.Wpf
{
    public class PhotoView : IPhotoView
    {
        public const string PhotoShapeName = "PhotoShapeName";

        private const string OpacityProperty = "Opacity";

        private readonly System.Windows.Controls.Canvas _canvas;

        private readonly double _width;

        private readonly double _height;

        private Photo _photo;

        private Rectangle _photoRectangle;

        public PhotoView(System.Windows.Controls.Canvas canvas, double width, double height)
        {
            _canvas = canvas;
            _width = width;
            _height = height;
        }

        public void Show(Photo photo)
        {
            _photo = photo;

            UiThread.Dispatch(() =>
            {
                RemovePreviousPhotoRectangleIfExists();

                CreatePhotoRectangle();
            });

            Refresh();
        }

        public void Refresh()
        {
            UiThread.Dispatch(() =>
            {
                System.Windows.Controls.Canvas.SetLeft(_photoRectangle, _photo.Center.X - _width / 2);
                System.Windows.Controls.Canvas.SetTop(_photoRectangle, _photo.Center.Y - _height / 2);
            });
        }

        public void Hide()
        {
            UiThread.Dispatch(() =>
            {
                _photoRectangle.Visibility = Visibility.Hidden;
            });
        }

        public void StartFadingOut(double durationInMs)
        {
            UiThread.Dispatch(() =>
            {
                var animation = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.0,
                    FillBehavior = FillBehavior.Stop,
                    BeginTime = TimeSpan.FromSeconds(0),
                    Duration = new Duration(TimeSpan.FromMilliseconds(durationInMs))
                };

                var storyboard = new Storyboard();
                storyboard.Children.Add(animation);

                Storyboard.SetTarget(animation, _photoRectangle);
                Storyboard.SetTargetProperty(animation, new PropertyPath(OpacityProperty));

                storyboard.Completed += delegate { _photoRectangle.Visibility = Visibility.Hidden; };

                storyboard.Begin();
            });
        }

        private void RemovePreviousPhotoRectangleIfExists()
        {
            if (_photoRectangle != null)
            {
                _canvas.Children.Remove(_photoRectangle);
            }
        }

        private void CreatePhotoRectangle()
        {
            ImageBrush image = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(_photo.ImagePath, UriKind.Relative)),
                Stretch = Stretch.Uniform,
                AlignmentX = AlignmentX.Center,
                AlignmentY = AlignmentY.Center,
            };

            _photoRectangle = new Rectangle
            {
                Name = PhotoShapeName,

                Height = _height,
                Width = _width,

                Fill = image,
                Opacity = 1.0,
            };

            _canvas.Children.Add(_photoRectangle);
        }
    }
}