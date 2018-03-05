using System.Collections.Generic;

namespace NationalityGame.Mechanics.Domain
{
    public class Game
    {
        public Board Board { get; }

        public IEnumerable<Bucket> Buckets { get; }

        public IEnumerable<Photo> Photos { get; }

        public Game(Board board, IEnumerable<Bucket> buckets, IEnumerable<Photo> photos)
        {
            Board = board;
            Buckets = buckets;
            Photos = photos;
        }
    }
}