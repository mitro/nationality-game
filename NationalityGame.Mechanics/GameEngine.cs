using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Mechanics.Scoring;
using NationalityGame.Mechanics.Utils;

namespace NationalityGame.Mechanics
{
    public class GameEngine : IGameEngine
    {
        private readonly Settings _settings;

        private readonly Game _game;

        private readonly IScoringStrategy _score;

        private Queue<Photo> _roundPhotos;

        private Bucket _chosenBucket;

        private Photo _runningPhoto;

        public GameEngine(
            Settings settings,
            Game game,
            IScoringStrategy score
            )
        {
            _settings = settings;
            _game = game;
            _score = score;
        }

        public event Action<int> RoundFinished;

        public event Action TickProcessed;

        public event Action<Photo> NextPhotoRun;

        public event Action<double> BucketChosen;

        public event Action<int> CurrentScoreChanged;

        public void StartNewRound()
        {
            _score.Reset();

            CurrentScoreChanged?.Invoke(0);

            _roundPhotos = GetRoundPhotos();

            RunNextPhoto();
        }

        public void ProcessTick(double msSinceLastTick)
        {
            _runningPhoto.Move(msSinceLastTick * _settings.VelocityInPxPerMs);

            if (_game.Board.PhotoIsOutside(_runningPhoto))
            {
                if (_chosenBucket != null)
                {
                    _score.ChangeScore(_chosenBucket.Matches(_runningPhoto));

                    CurrentScoreChanged?.Invoke(_score.TotalScore);
                }

                RunNextPhoto();
            }

            TickProcessed?.Invoke();
        }

        public void ProcessPan(Vector vector)
        {
            if (BucketAlreadyChosen())
            {
                return;
            }

            var chosenBucket = GetBucketPannedTo(vector);

            if (chosenBucket == null)
            {
                return;
            }

            _chosenBucket = chosenBucket;

            _runningPhoto.SetMovementVector(_runningPhoto.GetVectorTo(chosenBucket));

            BucketChosen?.Invoke(CalcTimeToReach(chosenBucket));
        }

        private bool BucketAlreadyChosen()
        {
            return _chosenBucket != null;
        }

        private double CalcTimeToReach(Bucket chosenBucket)
        {
            return _runningPhoto.GetVectorTo(chosenBucket).Length / _settings.VelocityInPxPerMs;
        }

        private Bucket GetBucketPannedTo(Vector vector)
        {
            return _game.Buckets
                .Select(bucket => new
                {
                    Bucket = bucket,
                    Angle = Math.Abs(Vector.AngleBetween(vector, _runningPhoto.GetVectorTo(bucket)))
                })
                .Where(bucketData => bucketData.Angle <= 20)
                .OrderBy(bucketData => bucketData.Angle)
                .Select(bucketData => bucketData.Bucket)
                .FirstOrDefault();
        }

        private void RunNextPhoto()
        {
            _chosenBucket = null;

            if (NoPhotosLeft())
            {
                RoundFinished?.Invoke(_score.TotalScore);

                return;
            }

            _runningPhoto = _roundPhotos.Dequeue();

            _runningPhoto.SetMovementVector(new Vector(0, 1));

            NextPhotoRun?.Invoke(_runningPhoto);
        }

        private bool NoPhotosLeft()
        {
            return !_roundPhotos.Any();
        }

        private Queue<Photo> GetRoundPhotos()
        {
            var clonedPhotos = _game.Photos.Select(p => p.Clone());

            if (_settings.ShufflePhotos)
            {
                clonedPhotos = clonedPhotos.Shuffle();
            }

            return new Queue<Photo>(clonedPhotos);
        }

        public class Settings
        {
            public bool ShufflePhotos { get; }

            public double VelocityInPxPerMs { get; }

            public Settings(bool shufflePhotos, double velocityInPxPerMs)
            {
                ShufflePhotos = shufflePhotos;
                VelocityInPxPerMs = velocityInPxPerMs;
            }
        }
    }
}