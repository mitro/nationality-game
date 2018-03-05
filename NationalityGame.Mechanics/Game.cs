using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Mechanics.Utils;

namespace NationalityGame.Mechanics
{
    public class Game : IGame
    {
        private readonly Settings _settings;

        private readonly Board _board;

        public IEnumerable<Bucket> Buckets { get; }

        private readonly IEnumerable<Photo> _photos;

        private readonly IScoringStrategy _score;

        private Bucket _chosenBucket;

        private Queue<Photo> _roundPhotos;

        private Photo _runningPhoto;

        public Game(
            Settings settings,
            Board board,
            IEnumerable<Bucket> buckets,
            IEnumerable<Photo> photos,
            IScoringStrategy score)
        {
            _settings = settings;
            _board = board;
            _photos = photos;
            _score = score;

            Buckets = buckets;
        }

        public event Action<int> RoundFinished;

        public event Action TickProcessed;

        public event Action<Photo> NextPhotoRun;

        public event Action<Bucket, double> BucketChosen;

        public void StartNewRound()
        {
            _score.Reset();

            _roundPhotos = GetRoundPhotos();

            RunNextPhoto();
        }

        public void ProcessTick(double msSinceLastTick)
        {
            _runningPhoto.Move(msSinceLastTick * _settings.VelocityInPxPerMs);

            if (_board.CheckPhotoLeft(_runningPhoto))
            {
                if (_chosenBucket != null)
                {
                    _score.Change(_chosenBucket.Matches(_runningPhoto));
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

            BucketChosen?.Invoke(chosenBucket, CalcTimeToReach(chosenBucket));
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
            return Buckets
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
            var clonedPhotos = _photos.Select(p => p.Clone());

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