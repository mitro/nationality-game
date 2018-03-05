using System;
using System.Windows;

namespace NationalityGame.Mechanics.Domain
{
    public class Bucket : GameObject
    {
        public string Nationality { get; }

        public Bucket(string nationality, Point center)
            : base(center)
        {
            if (string.IsNullOrWhiteSpace(nationality))
                throw new ArgumentException($"{nameof(nationality)} is null or empty");

            Nationality = nationality;
        }

        public bool Matches(Photo photo)
        {
            return Nationality.Equals(photo.Nationality, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}