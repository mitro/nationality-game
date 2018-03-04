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
        private readonly double _boardWidth;

        private readonly double _boardHeight;

        private double _velocity;

        private DateTime _lastTickAt;

        private readonly IList<Bucket> _buckets = new List<Bucket>();

        public IEnumerable<Bucket> Buckets => _buckets;

        public Photo Photo { get; private set; }

        public event Action<Bucket> PhotoDirectedToBucket;

        public event Action<double> VelocityChanged;

        public event Action GameStarted;

        public event Action<int> ScoreChanged;

        public event Action<int> RoundFinished;

        public event Action TickProcessed;

        private GameState _state;

        private Bucket _chosenBucket;

        private int _currentScore;

        private int _roundsLeft = 3;

        public Game(
            double boardWidth,
            double boardHeight)
        {
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;

            _buckets.Add(new Bucket("Japaneese", new Point(0, 0), 150, 150));
            _buckets.Add(new Bucket("Chinese", new Point(_boardWidth - 150, 0), 150, 150));
            _buckets.Add(new Bucket("Korean", new Point(_boardWidth - 150, _boardHeight - 150), 150, 150));
            _buckets.Add(new Bucket("Thai", new Point(0, _boardHeight - 150), 150, 150));

            _state = GameState.GameNotStarted;
        }

        public void Start()
        {
            if (_roundsLeft == 0)
            {
                RoundFinished?.Invoke(_currentScore);
                return;
            }

            _roundsLeft--;

            Photo = new Photo(new Point(_boardWidth / 2, 0), 150, 150, "Thai");

            Photo.SetMovementVector(new Vector(0, 1));

            _velocity = _boardHeight / 3000;

            VelocityChanged?.Invoke(_velocity);

            _lastTickAt = DateTime.Now;

            GameStarted?.Invoke();

            _state = GameState.PhotoFalling;
        }

        public void StartRound()
        {
            _roundsLeft = 3;

            _currentScore = 0;

            Start();
        }

        public void Tick()
        {
            var msElapsed = (DateTime.Now - _lastTickAt).TotalMilliseconds;

            Photo.Move(msElapsed * _velocity);

            _lastTickAt = DateTime.Now;

            if (_state == GameState.PhotoFalling)
            {
                if (Photo.Center.Y > _boardHeight)
                {
                    Start();
                }
            }
            else if (_state == GameState.BucketChosen)
            {
                if (Photo.GetVectorTo(_chosenBucket).Length < 5)
                {
                    if (Photo.Nationality.Equals(_chosenBucket.Nationality, StringComparison.InvariantCultureIgnoreCase))
                    {
                        _currentScore += 20;
                    }
                    else
                    {
                        _currentScore -= 5;
                    }

                    ScoreChanged?.Invoke(_currentScore);

                    Start();
                }
            }

            TickProcessed?.Invoke();
        }

        public void ProcessPan(Vector vector)
        {
            if (_state != GameState.PhotoFalling)
            {
                return;
            }

            var bucketPannedTo = _buckets
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

            _state = GameState.BucketChosen;

            Photo.SetMovementVector(Photo.GetVectorTo(bucketPannedTo));
            PhotoDirectedToBucket?.Invoke(bucketPannedTo);
        }
    }

    public enum GameState
    {
        Undefined,

        GameNotStarted,

        PhotoFalling,

        BucketChosen
    }
}