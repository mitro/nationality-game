using System.Windows;

namespace NationalityGame.Mechanics
{
    public class Bucket : GameObject
    {
        public string Nationality { get; }

        public Point Position { get; }

        public double Width { get; }

        public double Height { get; }

        public Bucket(string nationality, Point position, double width, double height)
            : base(new Point(position.X + width / 2, position.Y + height / 2))
        {
            Nationality = nationality;
            Position = position;
            Width = width;
            Height = height;
        }
    }
}