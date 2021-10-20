using SFML.Graphics;
using SFML.System;
using System;

namespace FigureManager.Figures
{
    public class Triangle : ConvexShape, IShape
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

        public void Draw(RenderWindow window)
        {
            window.Draw(this);
        }

        public IShape GetFrame
        {
            get
            {
                FloatRect FrameBounds = GetGlobalBounds();
                IShape frame = new Rectangle(
                    new Vector2f(FrameBounds.Left, FrameBounds.Top),
                    new Vector2f(FrameBounds.Left + FrameBounds.Width, FrameBounds.Top + FrameBounds.Height));
                frame.FillColor = Color.Transparent;
                frame.OutlineColor = Color.Black;
                frame.OutlineThickness = 2;

                return frame;
            }
        }

        public float GetSquare { get; protected set; }

        public float GetPerimeter { get; protected set; }

        public string GetDescription => string.Format("{0}: P={1}; S={2}", "Triangle", GetPerimeter, GetSquare);
    }
}
