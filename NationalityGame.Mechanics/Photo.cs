using System.Windows;

namespace NationalityGame.Mechanics
{
    public class Photo : GameObject
    {
        public double Width { get; }

        public double Height { get; }

        public Photo(Point center, double width, double height) : base(center)
        {
            Width = width;
            Height = height;
        }
    }
}