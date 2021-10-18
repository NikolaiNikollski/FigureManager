using SFML.Graphics;
using SFML.System;
using System;

namespace FigureManager.Figures
{
    class Triangle : ConvexShape, IShape
    {
        const int PointCount = 3;

        public Triangle(Vector2f p1, Vector2f p2, Vector2f p3) : base(PointCount)
        {
            SetPoint(0, p1);
            SetPoint(1, p2);
            SetPoint(2, p3);

            float a = (float)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
            float b = (float)Math.Sqrt(Math.Pow(p3.X - p2.X, 2) + Math.Pow(p3.Y - p2.Y, 2));
            float c = (float)Math.Sqrt(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p1.Y - p3.Y, 2));

            GetPerimeter = a + b + c;
            GetSquare = (float)Math.Sqrt(GetPerimeter / 2 * (GetPerimeter / 2 - a) * (GetPerimeter / 2 - b) * (GetPerimeter / 2 - c));
        }

        public float GetSquare { get; protected set; }

        public float GetPerimeter { get; protected set; }

        string IShape.GetDescription => string.Format("{0}: P={1}; S={2}", "Triangle", GetPerimeter, GetSquare);

        public void Draw(RenderWindow window)
        {
            window.Draw(this);
        }
    }
}
