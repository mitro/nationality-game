using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace NationalityGame.Mechanics
{
    public class Game
    {
        private readonly double _boardWidth;

        private readonly double _boardHeight;

        private double _velocity;

        private DateTime _lastTickAt;

        private readonly IList<Bucket> _buckets = new List<Bucket>();

        public Bucket TopLeftBucket { get; }

        public Bucket TopRightBucket { get; }

        public Bucket BottomRightBucket { get; }

        public Bucket BottomLeftBucket { get; }

        public Photo CurrentPhoto { get; private set; }

        public Game(
            double boardWidth,
            double boardHeight)
        {
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;

            TopLeftBucket = new Bucket("Japaneese", Corner.TopLeft, new Point(50, 50));
            TopRightBucket = new Bucket("Chinese", Corner.TopRight, new Point(_boardWidth - 50, 50));
            BottomRightBucket = new Bucket("Korean", Corner.BottomRight, new Point(_boardWidth - 50, _boardHeight - 50));
            BottomLeftBucket = new Bucket("Thai", Corner.BottomLeft, new Point(50, _boardHeight - 50));

            _buckets.Add(TopLeftBucket);
            _buckets.Add(TopRightBucket);
            _buckets.Add(BottomRightBucket);
            _buckets.Add(BottomLeftBucket);

            CurrentPhoto = new Photo(new Point((_boardWidth - 50) / 2, 50));

            _velocity = _boardHeight / 100000;

            _lastTickAt = DateTime.Now;
        }

        public void Tick()
        {
            var msElapsed = (DateTime.Now - _lastTickAt).TotalMilliseconds;

            CurrentPhoto.Center = new Point(
                CurrentPhoto.Center.X,
                CurrentPhoto.Center.Y + msElapsed * _velocity);

            _lastTickAt = DateTime.Now;
        }

        public void ProcessPan(Vector vector)
        {
            if (CurrentPhoto == null)
            {
                return;
            }

            // TODO Process situation when two angle to more distant object must be more
            var bucketPannedTo = _buckets
                .Select(bucket => new
                {
                    Bucket = bucket,
                    Angle = Math.Abs(Vector.AngleBetween(vector, CurrentPhoto.VectorTo(bucket)))
                })
                .Where(bucket => bucket.Angle <= 15)
                .OrderBy(bucket => bucket.Angle)
                .Select(bucket => bucket.Bucket)
                .FirstOrDefault();

            var message = bucketPannedTo?.Corner.ToString("G") ?? "No bucket";

            Debug.WriteLine(message);
        }
    }
}