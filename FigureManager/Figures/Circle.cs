using SFML.Graphics;
using SFML.System;
using System;

namespace FigureManager.Figures
{
    public class Circle : CircleShape, IShape
    {
        public Circle(Vector2f center, float radius) : base(radius)
        {
            Position = new Vector2f(center.X - radius, center.Y - radius);
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

        public float GetSquare => (float)(Math.Pow(Radius, 2) * Math.PI);

        public float GetPerimeter => (float)(2 * Math.Abs(Radius) * Math.PI);

        public string GetDescription => string.Format("{0}: P={1}; S={2}", "Circle", GetPerimeter, GetSquare);
    }
}
