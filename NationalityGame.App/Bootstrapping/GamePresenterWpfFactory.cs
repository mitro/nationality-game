using System.Linq;
using NationalityGame.App.Interactivity.Wpf;
using NationalityGame.App.Views.Wpf;
using NationalityGame.Mechanics;
using NationalityGame.Presentation;

namespace NationalityGame.App.Bootstrapping
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

            var recognizer = new UserInteractionRecognizer(window.GameCanvas);

            var ticker = new Ticker();

            return new GamePresenter(
                game,
                photoView,
                gameResultView,
                bucketViews,
                ticker,
                recognizer);
        }
    }
}