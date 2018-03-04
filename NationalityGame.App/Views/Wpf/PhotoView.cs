using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NationalityGame.Mechanics;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Presentation.Views;

namespace NationalityGame.App.Rendering.Canvas
{
    public class PhotoView : IPhotoView
    {
        private readonly Game _game;
        private readonly System.Windows.Controls.Canvas _canvas;

        private Photo _photo;

        private Rectangle _rectangle;

        private double _velocity;

        private bool _settingOpacity;

        private double _opacityStep;

        public PhotoView(Game game, System.Windows.Controls.Canvas canvas)
        {
            _game = game;
            _canvas = canvas;

            game.VelocityChanged += GameOnVelocityChanged;
            game.PhotoDirectedToBucket += GameOnPhotoDirectedToBucket;
            game.GameStarted += GameOnGameStarted;
        }

        private void GameOnGameStarted()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _settingOpacity = false;

                if (_rectangle != null)
                {
                    _canvas.Children.Remove(_rectangle);
                }

                _photo = _game.Photo;

                ImageBrush image = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("d:\\face1.jpg")),
                    Stretch = Stretch.Uniform,
                    AlignmentX = AlignmentX.Center,
                    AlignmentY = AlignmentY.Center,
                };

                _rectangle = new Rectangle
                {
                    Height = _photo.Height,
                    Width = _photo.Width,

                    Fill = image,
                    Opacity = 1.0,
                };

                _canvas.Children.Add(_rectangle);
            });
        }

        private void GameOnVelocityChanged(double velocity)
        {
            _velocity = velocity;
        }

        private void GameOnPhotoDirectedToBucket(Bucket bucket)
        {
            _settingOpacity = true;

            _opacityStep = 30.0 / (_photo.GetVectorTo(bucket).Length / _velocity);

            Debug.WriteLine(_opacityStep);
        }

        public void Render()
        {
            //if (_settingOpacity)
            //{
            //    _rectangle.Opacity -= _opacityStep;
            //}

            //System.Windows.Controls.Canvas.SetLeft(_rectangle, _photo.Center.X - _photo.Width / 2);
            //System.Windows.Controls.Canvas.SetTop(_rectangle, _photo.Center.Y - _photo.Height / 2);
        }

        public void Update()
        {
            UiThread.Dispatch(() =>
            {
                if (_settingOpacity)
                {
                    _rectangle.Opacity -= _opacityStep;
                }

                System.Windows.Controls.Canvas.SetLeft(_rectangle, _photo.Center.X - _photo.Width / 2);
                System.Windows.Controls.Canvas.SetTop(_rectangle, _photo.Center.Y - _photo.Height / 2);
            });
        }
    }
}