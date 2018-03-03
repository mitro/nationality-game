using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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

        private GameState _state;

        public Game(
            double boardWidth,
            double boardHeight)
        {
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;

            _buckets.Add(new Bucket("Japaneese", new Point(50, 50)));
            _buckets.Add(new Bucket("Chinese", new Point(_boardWidth - 50, 50)));
            _buckets.Add(new Bucket("Korean", new Point(_boardWidth - 50, _boardHeight - 50)));
            _buckets.Add(new Bucket("Thai", new Point(50, _boardHeight - 50)));

            _state = GameState.GameNotStarted;
        }

        public void Start()
        {
            Photo = new Photo(new Point((_boardWidth - 50) / 2, 0), 100, 100);

            Photo.SetMovementVector(new Vector(0, 1));

            _velocity = _boardHeight / 3000;

            VelocityChanged?.Invoke(_velocity);

            _lastTickAt = DateTime.Now;

            GameStarted?.Invoke();

            _state = GameState.PhotoFalling;
        }

        public void Tick()
        {
            var msElapsed = (DateTime.Now - _lastTickAt).TotalMilliseconds;

            Photo.Move(msElapsed * _velocity);

            _lastTickAt = DateTime.Now;

            if (Photo.Center.Y > _boardHeight)
            {
                Start();
            }
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
                .Where(bucketData => bucketData.Angle <= 30)
                .OrderBy(bucketData => bucketData.Angle)
                .Select(bucketData => bucketData.Bucket)
                .FirstOrDefault();

            if (bucketPannedTo == null)
            {
                return;
            }

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