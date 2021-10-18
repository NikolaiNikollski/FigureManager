using SFML.Graphics;
using SFML.System;
using System;

namespace FigureManager.Figures
{
    class Triangle : ConvexShape, IMathCalculable
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

            Perimeter = a + b + c;
            Square = (float)Math.Sqrt(Perimeter / 2 * (Perimeter / 2 - a) * (Perimeter / 2 - b) * (Perimeter / 2 - c));
        }

        public float Square { get; protected set; }

        public float Perimeter { get; protected set; }

        public string GetDescription()
        {
            return string.Format("{0}: P={1}; S={2}", "Triangle", Perimeter, Square);
        }
    }
}
