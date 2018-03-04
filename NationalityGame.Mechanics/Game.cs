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

        private double _velocity;

        public IEnumerable<Bucket> Buckets { get; }

        public event Action<Bucket, double> BucketSelected;

        public event Action<Photo> NextPhotoSent;

        public event Action<int> RoundFinished;

        public event Action TickProcessed;

        private Bucket _chosenBucket;

        private int _currentScore;

        private Queue<Photo> _roundPhotos;

        private Photo _currentPhoto;

        private readonly IEnumerable<Photo> _photos;

        public Game(
            Board board,
            IEnumerable<Bucket> buckets,
            IEnumerable<Photo> photos)
        {
            _board = board;

            Buckets = buckets;

            _photos = photos;
        }

        public void StartRound()
        {
            _currentScore = 0;

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
                    UpdateScore();

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

        private void UpdateScore()
        {
            if (_currentPhoto.Nationality.Equals(_chosenBucket.Nationality, StringComparison.InvariantCultureIgnoreCase))
            {
                _currentScore += 20;
            }
            else
            {
                _currentScore -= 5;
            }
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
                RoundFinished?.Invoke(_currentScore);

                return;
            }

            _currentPhoto = _roundPhotos.Dequeue();

            _currentPhoto.SetMovementVector(new Vector(0, 1));

            _velocity = _board.Height / 3000;

            NextPhotoSent?.Invoke(_currentPhoto);
        }
    }
}