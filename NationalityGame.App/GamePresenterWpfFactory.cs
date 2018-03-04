using System.Linq;
using NationalityGame.App.Rendering.Canvas;
using NationalityGame.Mechanics;

namespace NationalityGame.App
{
    public class GamePresenterWpfFactory
    {
        public GamePresenter Create(Game game, GameWindow window)
        {
            var photoView = new PhotoView(game, window.GameCanvas);

            var gameResultView = new GameResultView(window.GameCanvas);

            var bucketViews = game.Buckets
                .Select(bucket => new BucketView(bucket, window.GameCanvas))
                .ToList();

            return new GamePresenter(
                game,
                photoView,
                gameResultView,
                bucketViews);
        }
    }
}