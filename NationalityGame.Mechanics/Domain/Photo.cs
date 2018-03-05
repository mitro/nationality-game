using System.Windows;

namespace NationalityGame.Mechanics.Domain
{
    public class Photo : GameObject
    {
        public string Nationality { get; }

        public string ImagePath { get; }

        public Photo(Point center, string nationality, string imagePath) : base(center)
        {
            Nationality = nationality;
            ImagePath = imagePath;
        }

        public Photo Clone()
        {
            return new Photo(Center, Nationality, ImagePath);
        }
    }
}