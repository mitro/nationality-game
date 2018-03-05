using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NationalityGame.Mechanics.Domain;

namespace NationalityGame.Mechanics
{
    // TODO Make full screen
    // TODO Add "allowMindChanging"
    public class Game
    {
        private readonly Board _board;

        private readonly double _velocityInPxPerMs;

        public IEnumerable<Bucket> Buckets { get; }

        private readonly IEnumerable<Photo> _photos;

        private readonly Score _score;

        private Bucket _chosenBucket;

        private Queue<Photo> _roundPhotos;

        private Photo _runningPhoto;

        public Game(
            Board board,
            IEnumerable<Bucket> buckets,
            IEnumerable<Photo> photos,
            Score score,
            double velocityInPxPerMs)
        {
            _board = board;

            Buckets = buckets;

            _photos = photos;

            _score = score;

            _velocityInPxPerMs = velocityInPxPerMs;
        }

        public event Action<int> RoundFinished;

        public event Action TickProcessed;

        public event Action<Photo> NextPhotoRun;

        public event Action<Bucket, double> BucketChosen;

        public void StartNewRound()
        {
            _score.Reset();

            _roundPhotos = new Queue<Photo>(_photos.Select(p => p.Clone()));

            RunNextPhoto();
        }

        public void ProcessTick(double msSinceLastTick)
        {
            _runningPhoto.Move(msSinceLastTick * _velocityInPxPerMs);

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
            if (_chosenBucket != null)
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

        private double CalcTimeToReach(Bucket chosenBucket)
        {
            return _runningPhoto.GetVectorTo(chosenBucket).Length / _velocityInPxPerMs;
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
    }
}