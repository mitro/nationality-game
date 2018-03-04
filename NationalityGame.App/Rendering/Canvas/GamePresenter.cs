using System.Collections.Generic;
using NationalityGame.Mechanics;
using NationalityGame.Presentation.Views;

namespace NationalityGame.App.Rendering.Canvas
{
    public class GamePresenter
    {
        private readonly Game _game;

        private readonly IPhotoView _photoView;

        private readonly IGameResultView _gameResultView;

        private readonly IEnumerable<IBucketView> _bucketViews;

        public GamePresenter(
            Game game,
            IPhotoView photoView,
            IGameResultView gameResultView,
            IEnumerable<IBucketView> bucketViews)
        {
            _game = game;

            _photoView = photoView;
            _gameResultView = gameResultView;
            _bucketViews = bucketViews;

            _game.TickProcessed += GameOnTickProcessed;
            _game.RoundFinished += GameOnRoundFinished;

            _gameResultView.PlayAgainExecuted += GameResultViewOnNewGameRequested;
        }

        public void Prepare()
        {
            foreach (var bucketView in _bucketViews)
            {
                bucketView.Show();
            }
        }

        private void GameOnTickProcessed()
        {
            _photoView.Update();
        }

        private void GameOnRoundFinished(int score)
        {
            _gameResultView.Show(score);
        }

        private void GameResultViewOnNewGameRequested()
        {
            _gameResultView.Hide();

            _game.StartRound();
        }
    }
}