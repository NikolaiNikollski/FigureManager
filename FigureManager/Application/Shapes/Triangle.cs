using SFML.Graphics;
using SFML.System;
using System;

namespace FigureManager.Shapes
{
    public class Triangle : MyShape
    {
        private const float DefaultRadius = 50;
        private const int PointCount = 3;

        private float Side0 { get; }
        private float Side1 { get; }
        private float Side2 { get; }

        public Vector2f p1;
        public Vector2f p2;
        public Vector2f p3;

        public Triangle(Vector2f р1, Vector2f р2, Vector2f р3) : base(new ConvexShape(PointCount))
        {
            p1 = р1;
            p2 = р2;
            p3 = р3;
            ConvexShape baseShape = (ConvexShape)base.Shape;
            baseShape.SetPoint(0, p1);
            baseShape.SetPoint(1, p2);
            baseShape.SetPoint(2, p3);

            Side0 = (float)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
            Side1 = (float)Math.Sqrt(Math.Pow(p3.X - p2.X, 2) + Math.Pow(p3.Y - p2.Y, 2));
            Side2 = (float)Math.Sqrt(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p1.Y - p3.Y, 2));
            Type = ShapeType.Triangle;
        }

        public Triangle(Vector2f center) : base(new ConvexShape(PointCount))
        {
            ConvexShape baseShape = (ConvexShape)base.Shape;
            p1 = new Vector2f(center.X, center.Y - DefaultRadius);
            p2 = new Vector2f(center.X - DefaultRadius * (float)Math.Sqrt(3) / 2, center.Y + DefaultRadius / 2);
            p3 = new Vector2f(center.X + DefaultRadius *(float)Math.Sqrt(3) / 2, center.Y + DefaultRadius / 2);
            baseShape.SetPoint(0, p1);
            baseShape.SetPoint(1, p2);
            baseShape.SetPoint(2, p3);

            Side0 = (float)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
            Side1 = (float)Math.Sqrt(Math.Pow(p3.X - p2.X, 2) + Math.Pow(p3.Y - p2.Y, 2));
            Side2 = (float)Math.Sqrt(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p1.Y - p3.Y, 2));
            Type = ShapeType.Triangle;
        }

        public override float GetPerimeter { get => Side0 + Side1 + Side2; }

        public override float GetSquare { get => (float)Math.Sqrt(GetPerimeter / 2 * (GetPerimeter / 2 - Side0) * (GetPerimeter / 2 - Side1) * (GetPerimeter / 2 - Side2)); }

        public override object Clone()
        {
            Triangle clone = new Triangle(p1, p2, p3);

            clone.FillColor = FillColor;
            clone.Position = new Vector2f(Position.X, Shape.Position.Y);
            clone.OutlineColor = OutlineColor;
            clone.OutlineThickness = OutlineThickness;

            return clone;
        }
    }
}
