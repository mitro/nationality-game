using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using NationalityGame.App.Rendering.Canvas;
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
                .Select(bucket => new BucketRenderer(_game, bucket, _canvas))
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