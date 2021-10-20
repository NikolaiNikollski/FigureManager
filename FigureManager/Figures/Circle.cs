using SFML.Graphics;
using SFML.System;
using System;

namespace FigureManager.Figures
{
    public class Circle : MyShape
    {
        public float GetRadius { get; }

        public Circle(Vector2f center, float radius) : base(new CircleShape(radius))
        {
            Shape.Position = new Vector2f(center.X - radius, center.Y - radius);

            GetRadius = radius;
        }

        public override float GetSquare => (float)(Math.Pow(GetRadius, 2) * Math.PI);

        public override float GetPerimeter => (float)(2 * Math.Abs(GetRadius) * Math.PI);
    }
}
