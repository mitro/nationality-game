using System;
using System.Collections.Generic;
using System.Windows;
using NationalityGame.Mechanics;
using NationalityGame.Mechanics.Domain;
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
            _game.NextPhotoRun += GameOnNextPhotoRun;
            _game.BucketChosen += GameOnBucketChosen;

            _photoView = photoView;

            _gameResultView = gameResultView;
            _gameResultView.PlayAgainRequested += GameResultViewOnPlayAgainRequested;

            _bucketViews = bucketViews;

            _ticker = ticker;
            _ticker.Ticked += TickerOnTicked;

            userInteractionRecognizer.PanRecognized += UserInteractionRecognizerOnPanRecognized;
        }

        private void GameOnBucketChosen(Bucket bucket, double timeToReachInMs)
        {
            _photoView.StartFadingOut(timeToReachInMs);
        }

        private void GameOnNextPhotoRun(Photo photo)
        {
            _photoView.Show(photo);
        }

        public void Start()
        {
            AssertNotStartedYet();

            foreach (var bucketView in _bucketViews)
            {
                bucketView.Show();
            }

            _game.StartNewRound();

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
            _photoView.Refresh();
        }

        private void GameOnRoundFinished(int score)
        {
            _ticker.Stop();

            _photoView.Hide();

            _gameResultView.Show(score);
        }

        private void UserInteractionRecognizerOnPanRecognized(Vector vector)
        {
            _game.ProcessPan(vector);
        }

        private void GameResultViewOnPlayAgainRequested()
        {
            _gameResultView.Hide();

            _game.StartNewRound();

            _ticker.Start();
        }

        private void TickerOnTicked(double msSinceLastTick)
        {
            _game.ProcessTick(msSinceLastTick);
        }
    }
}