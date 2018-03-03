using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using NationalityGame.App.Rendering.Canvas;
using NationalityGame.Mechanics;

namespace NationalityGame.App.Rendering
{
    public class GameRenderer
    {
        private readonly System.Windows.Controls.Canvas _canvas;
        private readonly Label _scoreLabel;

        private readonly Game _game;

        private List<BucketRenderer> _bucketRenderers = new List<BucketRenderer>();

        private PhotoRenderer _photoRenderer;

        public GameRenderer(System.Windows.Controls.Canvas canvas, Label scoreLabel, Game game)
        {
            _canvas = canvas;
            _scoreLabel = scoreLabel;
            _game = game;

            _game.ScoreChanged += GameOnScoreChanged;

            AddGameObjects();
        }

        private void GameOnScoreChanged(int score)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _scoreLabel.Content = score.ToString();
            });
        }

        private void AddGameObjects()
        {
            _canvas.Children.Clear();

            _bucketRenderers = _game.Buckets
                .Select(bucket => new BucketRenderer(bucket, _canvas))
                .ToList();

            _photoRenderer = new PhotoRenderer(_game, _canvas);
        }

        public void Render()
        {
            _bucketRenderers.ForEach(bucket => bucket.Render());
            _photoRenderer.Render();
        }
    }
}