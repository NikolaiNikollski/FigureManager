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
            GetRadius = Math.Abs(radius);
            Type = ShapeType.Circle;
        }

        public override float GetSquare => (float)(Math.Pow(GetRadius, 2) * Math.PI);

        public override float GetPerimeter => (float)(2 * Math.Abs(GetRadius) * Math.PI);

        public override object Clone()
        {
            Circle clone = new Circle(new Vector2f(0, 0));

            clone.FillColor = FillColor;
            clone.Position = new Vector2f(Position.X, Shape.Position.Y);
            clone.OutlineColor = OutlineColor;
            clone.OutlineThickness = OutlineThickness;

            return clone;
        }
    }
}
