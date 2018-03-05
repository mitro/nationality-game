using System;
using System.Windows;

namespace NationalityGame.Mechanics.Domain
{
    public class Bucket : GameObject
    {
        public string Nationality { get; }

        public Point Position { get; }

        public Bucket(string nationality, Point position)
            : base(position)
        {
            Nationality = nationality;
            Position = position;
        }

        public bool Matches(Photo photo)
        {
            return Nationality.Equals(photo.Nationality, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}