using System.Reflection;
using System.Windows;

namespace NationalityGame.Mechanics
{
    public class GameObject
    {
        public Point Center { get; set; }

        public GameObject(Point center)
        {
            Center = center;
        }

        public Vector VectorTo(GameObject gameObject)
        {
            return new Vector(gameObject.Center.X - Center.X, gameObject.Center.Y - Center.Y);
        }
    }
}