using SFML.Graphics;
using SFML.System;
using System;

namespace FigureManager.Shapes
{
    public class Circle : MyShape
    {
        private const int DefaultRadius = 50;
        public float GetRadius { get; }

        public Circle(Vector2f center, float radius = DefaultRadius) : base(new CircleShape(radius))
        {
            Shape.Position = new Vector2f(center.X - radius, center.Y - radius);
            GetRadius = radius;
            Type = ShapeType.Circle;
        }

        public override float GetSquare => (float)(Math.Pow(GetRadius, 2) * Math.PI);

        public override float GetPerimeter => (float)(2 * Math.Abs(GetRadius) * Math.PI);
    }
}
