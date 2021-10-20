using SFML.Graphics;
using SFML.System;
using System;

namespace FigureManager.Figures
{
    public class Triangle : MyShape
    {
        private const int PointCount = 3;

        public float Side0 { get; }

        public float Side1 { get; }

        public float Side2 { get; }

        public Triangle(Vector2f p1, Vector2f p2, Vector2f p3) : base(new ConvexShape(PointCount))
        {
            ConvexShape baseShape = (ConvexShape)base.Shape;
            baseShape.SetPoint(0, p1);
            baseShape.SetPoint(1, p2);
            baseShape.SetPoint(2, p3);

            Side0 = (float)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
            Side1 = (float)Math.Sqrt(Math.Pow(p3.X - p2.X, 2) + Math.Pow(p3.Y - p2.Y, 2));
            Side2 = (float)Math.Sqrt(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p1.Y - p3.Y, 2));
        }

        public override float GetPerimeter { get => Side0 + Side1 + Side2; }

        public override float GetSquare { get => (float)Math.Sqrt(GetPerimeter / 2 * (GetPerimeter / 2 - Side0) * (GetPerimeter / 2 - Side1) * (GetPerimeter / 2 - Side2)); }
    }
}
