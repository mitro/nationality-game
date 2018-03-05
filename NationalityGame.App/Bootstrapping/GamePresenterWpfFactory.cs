using System.Linq;
using NationalityGame.App.Interactivity.Wpf;
using NationalityGame.App.Views.Wpf;
using NationalityGame.Mechanics;
using NationalityGame.Mechanics.Configuration;
using NationalityGame.Presentation;

namespace NationalityGame.App.Bootstrapping
{
    public class GamePresenterWpfFactory
    {
        public GamePresenter Create(Game game, GameSettings settings, GameWindow window)
        {
            var photoView = new PhotoView(window.GameCanvas, settings.Appearance.PhotoWidth, settings.Appearance.PhotoHeight);

            var gameResultView = new GameResultView(window.GameCanvas);

            var bucketViews = game.Buckets
                .Select(bucket => new BucketView(
                    bucket,
                    window.GameCanvas,
                    settings.Appearance.BucketWidth,
                    settings.Appearance.BucketHeight))
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