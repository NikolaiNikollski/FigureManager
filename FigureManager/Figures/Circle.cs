using SFML.Graphics;
using SFML.System;
using System;

namespace FigureManager.Figures
{
    class Circle : CircleShape, IShape
    {
        public Circle(Vector2f center, float radius) : base(radius)
        {
            Position = new Vector2f(center.X - radius, center.Y - radius);
        }

        public float GetSquare => (float)(Math.Pow(Radius, 2) * Math.PI);

        public float GetPerimeter => (float)(2 * Math.Abs(Radius) * Math.PI);

        string IShape.GetDescription => string.Format("{0}: P={1}; S={2}", "Circle", GetPerimeter, GetSquare);

        public void Draw(RenderWindow window)
        {
            window.Draw(this);
        }
    }
}
