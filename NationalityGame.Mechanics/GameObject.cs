using System;
using System.Windows;

namespace NationalityGame.Mechanics
{
    public abstract class GameObject
    {
        private double _sin;
        private double _cos;

        public Point Center { get; private set; }

        protected GameObject(Point center)
        {
            Center = center;
        }

        public Vector GetVectorTo(GameObject gameObject)
        {
            return new Vector(gameObject.Center.X - Center.X, gameObject.Center.Y - Center.Y);
        }

        public void SetMovementVector(Vector vector)
        {
            var originVector = new Vector(0, 1);

            var angle = Vector.AngleBetween(originVector, vector) * Math.PI / 180;

            _sin = Math.Sin(angle);
            _cos = Math.Cos(angle);
        }

        public void Move(double distance)
        {
            var deltaX = -distance * _sin;
            var deltaY = distance * _cos;

            Center = new Point(
                Center.X + deltaX,
                Center.Y + deltaY);
        }
    }
}