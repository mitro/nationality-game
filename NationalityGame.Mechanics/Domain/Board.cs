using System;

namespace NationalityGame.Mechanics.Domain
{
    public class Board
    {
        public double Width { get; }

        public double Height { get; }

        public Board(double width, double height)
        {
            if (width <= 0) throw new ArgumentException($"{nameof(width)} is less or equal zero");
            if (height <= 0) throw new ArgumentException($"{nameof(height)} is less or equal zero");

            Width = width;
            Height = height;
        }

        public bool PhotoIsOutside(Photo photo)
        {
            return photo.Center.X < 0 ||
                   photo.Center.X > Width ||
                   photo.Center.Y < 0 ||
                   photo.Center.Y > Height;
        }
    }
}