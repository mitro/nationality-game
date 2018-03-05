using System;
using System.Collections.Generic;
using System.Windows;
using NationalityGame.Mechanics;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Presentation.Interactivity;
using NationalityGame.Presentation.Views;

namespace NationalityGame.Presentation
{
    public class GamePresenter : IGamePresenter
    {
        private bool _isStarted;

        private readonly IGameEngine _gameEngine;

        private readonly IPhotoView _photoView;

        private readonly IGameResultView _gameResultView;

        private readonly ICurrentScoreView _currentScoreView;

        private readonly IEnumerable<IBucketView> _bucketViews;

        private readonly ITicker _ticker;

        private readonly IGestureRecognizer _gestureRecognizer;

        public GamePresenter(
            IGameEngine gameEngine,
            IPhotoView photoView,
            IGameResultView gameResultView,
            ICurrentScoreView currentScoreView,
            IEnumerable<IBucketView> bucketViews,
            ITicker ticker,
            IGestureRecognizer gestureRecognizer)
        {
            _gameEngine = gameEngine;
            _photoView = photoView;
            _gameResultView = gameResultView;
            _currentScoreView = currentScoreView;
            _bucketViews = bucketViews;
            _ticker = ticker;
            _gestureRecognizer = gestureRecognizer;
        }

        public void Start()
        {
            AssertNotStartedYet();

            SubscribeToEvents();

            foreach (var bucketView in _bucketViews)
            {
                bucketView.Show();
            }

            _gameEngine.StartNewRound();

            _ticker.Start();
        }

        private void SubscribeToEvents()
        {
            _gameEngine.TickProcessed += GameEngineOnTickProcessed;
            _gameEngine.RoundFinished += GameEngineOnRoundFinished;
            _gameEngine.NextPhotoRun += GameEngineOnNextPhotoRun;
            _gameEngine.BucketChosen += GameEngineOnBucketChosen;
            _gameEngine.CurrentScoreChanged += GameEngineOnCurrentScoreChanged;

            _gameResultView.PlayAgainRequested += GameResultViewOnPlayAgainRequested;

            _ticker.Ticked += TickerOnTicked;

            _gestureRecognizer.PanRecognized += GestureRecognizerOnPanRecognized;
        }

        private void AssertNotStartedYet()
        {
            if (_isStarted)
            {
                throw new InvalidOperationException($"{nameof(GamePresenter)} is already started. Unable to proceed. Check the code.");
            }

            _isStarted = true;
        }

        private void GestureRecognizerOnPanRecognized(Vector vector)
        {
            _gameEngine.ProcessPan(vector);
        }

        private void TickerOnTicked(double msSinceLastTick)
        {
            _gameEngine.ProcessTick(msSinceLastTick);
        }

        private void GameResultViewOnPlayAgainRequested()
        {
            _gameResultView.Hide();

            _gameEngine.StartNewRound();

            _ticker.Start();
        }

        private void GameEngineOnTickProcessed()
        {
            _photoView.Refresh();
        }

        private void GameEngineOnRoundFinished(int score)
        {
            _ticker.Stop();

            _photoView.Hide();

            _currentScoreView.Hide();

            _gameResultView.Show(score);
        }

        private void GameEngineOnBucketChosen(double timeToReachInMs)
        {
            _photoView.StartFadingOut(timeToReachInMs);
        }

        private void GameEngineOnNextPhotoRun(Photo photo)
        {
            _photoView.Show(photo);
        }

        private void GameEngineOnCurrentScoreChanged(int currentScore)
        {
            _currentScoreView.Show(currentScore);
        }
    }
}