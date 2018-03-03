using System.Windows;

namespace NationalityGame.Mechanics
{
    public class Bucket : GameObject
    {
        public string Label { get; }

        public Point Position { get; }

        public double Width { get; }

        public double Height { get; }

        public Bucket(string label, Point position, double width, double height)
            : base(new Point(position.X + width / 2, position.Y + height / 2))
        {
            Label = label;
            Position = position;
            Width = width;
            Height = height;
        }
    }
}