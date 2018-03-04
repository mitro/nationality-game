using System;
using System.Collections.Generic;
using System.Windows;
using NationalityGame.Mechanics;
using NationalityGame.Presentation.Interactivity;
using NationalityGame.Presentation.Views;

namespace NationalityGame.Presentation
{
    public class GamePresenter
    {
        private bool _isStarted;

        private readonly Game _game;

        private readonly IPhotoView _photoView;

        private readonly IGameResultView _gameResultView;

        private readonly IEnumerable<IBucketView> _bucketViews;

        private readonly ITicker _ticker;

        public GamePresenter(
            Game game,
            IPhotoView photoView,
            IGameResultView gameResultView,
            IEnumerable<IBucketView> bucketViews,
            ITicker ticker,
            IUserInteractionRecognizer userInteractionRecognizer)
        {
            _game = game;
            _game.TickProcessed += GameOnTickProcessed;
            _game.RoundFinished += GameOnRoundFinished;

            _photoView = photoView;

            _gameResultView = gameResultView;
            _gameResultView.PlayAgainExecuted += GameResultViewOnNewGameRequested;

            _bucketViews = bucketViews;

            _ticker = ticker;
            _ticker.Ticked += TickerOnTicked;

            userInteractionRecognizer.PanRecognized += UserInteractionRecognizerOnPanRecognized;
        }

        public void Start()
        {
            AssertNotStartedYet();

            foreach (var bucketView in _bucketViews)
            {
                bucketView.Show();
            }

            _game.Start();

            _ticker.Start();
        }

        private void AssertNotStartedYet()
        {
            if (_isStarted)
            {
                throw new InvalidOperationException($"{nameof(GamePresenter)} is already started. Unable to proceed. Check the code.");
            }

            _isStarted = true;
        }

        private void GameOnTickProcessed()
        {
            _photoView.Update();
        }

        private void GameOnRoundFinished(int score)
        {
            _ticker.Stop();

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

            _ticker.Start();
        }

        private void TickerOnTicked()
        {
            _game.ProcessTick();
        }
    }
}