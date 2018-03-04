using System.Windows;

namespace NationalityGame.Mechanics.Domain
{
    public class Photo : GameObject
    {
        public double Width { get; }

        public double Height { get; }

        public string Nationality { get; set; }

        public Photo(Point center, double width, double height, string nationality) : base(center)
        {
            Width = width;
            Height = height;
            Nationality = nationality;
        }
    }
}