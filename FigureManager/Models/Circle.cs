using SFML.Graphics;
using SFML.System;
using System;

namespace FigureManager.Decorator
{
    class Circle : CircleShape, IMathCalculable
    {
        public Circle(Vector2f center, float radius) : base(radius)
        {
            Position = new Vector2f(center.X - radius, center.Y - radius);
        }

        public float Square => (float)(Math.Pow(Radius, 2) * Math.PI);

        public float Perimeter => (float)(2 * Radius * Math.PI);

        public string GetDescription()
        {
            return string.Format("{0}: P={1}; S={2}", "Circle", Perimeter, Square);
        }
    }
}
