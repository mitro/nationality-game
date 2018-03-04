namespace NationalityGame.Mechanics.Domain
{
    public class Board
    {
        public double Width { get; }

        public double Height { get; }

        public Board(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public bool ObjectIsInside(GameObject gameObject)
        {
            return
                gameObject.Center.X > 0 &&
                gameObject.Center.X < Width &&
                gameObject.Center.Y > 0 &&
                gameObject.Center.Y < Height;
        }
    }
}