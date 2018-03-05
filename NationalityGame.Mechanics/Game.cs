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

        private readonly double _velocity;

        public IEnumerable<Bucket> Buckets { get; }

        public event Action<Bucket, double> BucketSelected;

        public event Action<Photo> NextPhotoSent;

        public event Action<int> RoundFinished;

        public event Action TickProcessed;

        private Bucket _chosenBucket;

        private Queue<Photo> _roundPhotos;

        private Photo _currentPhoto;

        private readonly IEnumerable<Photo> _photos;

        private readonly Score _score;

        public Game(
            Board board,
            IEnumerable<Bucket> buckets,
            IEnumerable<Photo> photos,
            Score score,
            double velocity)
        {
            _board = board;

            Buckets = buckets;

            _photos = photos;

            _score = score;

            _velocity = velocity;
        }

        public void StartRound()
        {
            _score.Reset();

            _roundPhotos = new Queue<Photo>(_photos.Select(p => p.Clone()));

            SendNextPhoto();
        }

        public void ProcessTick(double msSinceLastTick)
        {
            _currentPhoto.Move(msSinceLastTick * _velocity);

            if (PhotoLeftBoard())
            {
                if (_chosenBucket == null)
                {
                    SendNextPhoto();
                }
                else if (_chosenBucket != null)
                {
                    _score.Change(_chosenBucket.Matches(_currentPhoto));

                    SendNextPhoto();
                }
            }

            TickProcessed?.Invoke();
        }

        public void ProcessPan(Vector vector)
        {
            if (_chosenBucket != null)
            {
                return;
            }

            var chosenBucket = Buckets
                .Select(bucket => new
                {
                    Bucket = bucket,
                    Angle = Math.Abs(Vector.AngleBetween(vector, _currentPhoto.GetVectorTo(bucket)))
                })
                .Where(bucketData => bucketData.Angle <= 20)
                .OrderBy(bucketData => bucketData.Angle)
                .Select(bucketData => bucketData.Bucket)
                .FirstOrDefault();

            if (chosenBucket == null)
            {
                return;
            }

            _chosenBucket = chosenBucket;

            _currentPhoto.SetMovementVector(_currentPhoto.GetVectorTo(chosenBucket));

            BucketSelected?.Invoke(chosenBucket, _currentPhoto.GetVectorTo(chosenBucket).Length / _velocity);
        }

        private bool PhotoLeftBoard()
        {
            return !_board.ObjectIsInside(_currentPhoto);
        }

        private void SendNextPhoto()
        {
            _chosenBucket = null;

            if (_roundPhotos.Count == 0)
            {
                RoundFinished?.Invoke(_score.TotalScore);

                return;
            }

            _currentPhoto = _roundPhotos.Dequeue();

            _currentPhoto.SetMovementVector(new Vector(0, 1));

            NextPhotoSent?.Invoke(_currentPhoto);
        }
    }
}