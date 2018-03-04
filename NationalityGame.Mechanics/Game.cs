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

        private DateTime _lastTickAt;

        public IEnumerable<Bucket> Buckets { get; }

        public Photo Photo { get; private set; }

        public event Action<Bucket> PhotoDirectedToBucket;

        public event Action<double> VelocityChanged;

        public event Action GameStarted;

        public event Action<int> RoundFinished;

        public event Action TickProcessed;

        private Bucket _chosenBucket;

        private int _currentScore;

        private int _roundsLeft = 3;

        public Game(Board board, IEnumerable<Bucket> buckets)
        {
            _board = board;

            Buckets = buckets;
        }

        public void Start()
        {
            _chosenBucket = null;

            if (_roundsLeft == 0)
            {
                RoundFinished?.Invoke(_currentScore);

                return;
            }

            _roundsLeft--;

            Photo = new Photo(new Point(_board.Width / 2, 0), "Thai");

            Photo.SetMovementVector(new Vector(0, 1));

            _velocity = _board.Height / 3000;

            VelocityChanged?.Invoke(_velocity);

            _lastTickAt = DateTime.Now;

            GameStarted?.Invoke();
        }

        public void StartRound()
        {
            _roundsLeft = 3;

            _currentScore = 0;

            Start();
        }

        public void ProcessTick()
        {
            var msElapsed = (DateTime.Now - _lastTickAt).TotalMilliseconds;

            Photo.Move(msElapsed * _velocity);

            _lastTickAt = DateTime.Now;

            if (PhotoLeftBoard())
            {
                if (_chosenBucket == null)
                {
                    Start();
                }
                else if (_chosenBucket != null)
                {
                    UpdateScore();

                    Start();
                }
            }

            TickProcessed?.Invoke();
        }

        private void UpdateScore()
        {
            if (Photo.Nationality.Equals(_chosenBucket.Nationality, StringComparison.InvariantCultureIgnoreCase))
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
            return !_board.ObjectIsInside(Photo);
        }

        public void ProcessPan(Vector vector)
        {
            if (_chosenBucket != null)
            {
                return;
            }

            var bucketPannedTo = Buckets
                .Select(bucket => new
                {
                    Bucket = bucket,
                    Angle = Math.Abs(Vector.AngleBetween(vector, Photo.GetVectorTo(bucket)))
                })
                .Where(bucketData => bucketData.Angle <= 20)
                .OrderBy(bucketData => bucketData.Angle)
                .Select(bucketData => bucketData.Bucket)
                .FirstOrDefault();

            if (bucketPannedTo == null)
            {
                return;
            }

            _chosenBucket = bucketPannedTo;

            Photo.SetMovementVector(Photo.GetVectorTo(bucketPannedTo));
            PhotoDirectedToBucket?.Invoke(bucketPannedTo);
        }
    }
}