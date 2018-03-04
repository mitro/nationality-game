using System.Collections.Generic;
using System.Windows;
using NationalityGame.Mechanics;
using NationalityGame.Presentation.Interacting;
using NationalityGame.Presentation.Views;

namespace NationalityGame.Presentation
{
    public class GamePresenter
    {
        private readonly Game _game;

        private readonly IPhotoView _photoView;

        private readonly IGameResultView _gameResultView;

        private readonly IEnumerable<IBucketView> _bucketViews;

        public GamePresenter(
            Game game,
            IUserInteractionRecognizer userInteractionRecognizer,
            IPhotoView photoView,
            IGameResultView gameResultView,
            IEnumerable<IBucketView> bucketViews)
        {
            _game = game;
            _game.TickProcessed += GameOnTickProcessed;
            _game.RoundFinished += GameOnRoundFinished;

            userInteractionRecognizer.PanRecognized += UserInteractionRecognizerOnPanRecognized;

            _photoView = photoView;

            _gameResultView = gameResultView;
            _gameResultView.PlayAgainExecuted += GameResultViewOnNewGameRequested;

            _bucketViews = bucketViews;
        }

        public void Start()
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

        private void UserInteractionRecognizerOnPanRecognized(Vector vector)
        {
            _game.ProcessPan(vector);
        }

        private void GameResultViewOnNewGameRequested()
        {
            _gameResultView.Hide();

            _game.StartRound();
        }
    }
}