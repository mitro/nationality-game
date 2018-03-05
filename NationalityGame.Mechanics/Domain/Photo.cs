using System;
using System.Windows;

namespace NationalityGame.Mechanics.Domain
{
    public class Photo : GameObject
    {
        public string Nationality { get; }

        public string ImagePath { get; }

        public Photo(Point center, string nationality, string imagePath) : base(center)
        {
            if (string.IsNullOrWhiteSpace(nationality))
                throw new ArgumentException($"{nameof(nationality)} is null or empty");
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentException($"{nameof(imagePath)} is null or empty");

            Nationality = nationality;
            ImagePath = imagePath;
        }

        public Photo Clone()
        {
            return new Photo(Center, Nationality, ImagePath);
        }
    }
}