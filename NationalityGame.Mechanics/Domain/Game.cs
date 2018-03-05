using System;
using System.Collections.Generic;
using System.Linq;

namespace NationalityGame.Mechanics.Domain
{
    public class Game
    {
        public Board Board { get; }

        public IEnumerable<Bucket> Buckets { get; }

        public IEnumerable<Photo> Photos { get; }

        public Game(Board board, IEnumerable<Bucket> buckets, IEnumerable<Photo> photos)
        {
            Board = board ?? throw new ArgumentException($"{nameof(board)} is null");
            Buckets = buckets ?? throw new ArgumentException($"{nameof(buckets)} is null");
            Photos = photos ?? throw new ArgumentException($"{nameof(photos)} is null");

            if (!buckets.Any()) throw new ArgumentException($"{nameof(buckets)} is empty");
            if (!photos.Any()) throw new ArgumentException($"{nameof(photos)} is empty");
        }
    }
}