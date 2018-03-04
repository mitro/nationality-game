using System.Collections.Generic;
using NationalityGame.Mechanics;

namespace NationalityGame.App.Rendering.Canvas
{
    public class CanvasPresenter: IView
    {
        private readonly GameWindow _window;

        private readonly Game _game;

        private readonly IList<IView> _renderers = new List<IView>();

        public CanvasPresenter(GameWindow window, Game game)
        {
            _window = window;
            _game = game;

            CreateObjectRenderers();
        }

        public void Render()
        {
            foreach (var renderer in _renderers)
            {
                renderer.Render();
            }
        }

        private void CreateObjectRenderers()
        {
            foreach (var bucket in _game.Buckets)
            {
                CreateBucketRenderer(bucket);
            }

            CreatePhotoRenderer(_game.Photo);
            CreateControlsRenderer();
        }

        private void CreateControlsRenderer()
        {
            var renderer = new ControlsView(_window.GameCanvas);

            _renderers.Add(renderer);
        }

        private void CreatePhotoRenderer(Photo photo)
        {
            var renderer = new PhotoView(_game, _window.GameCanvas);

            _renderers.Add(renderer);
        }

        private void CreateBucketRenderer(Bucket bucket)
        {
            var renderer = new BucketView(bucket, _window.GameCanvas);

            _renderers.Add(renderer);
        }
    }
}