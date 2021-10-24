using SFML.Graphics;
using SFML.System;
using System;

namespace FigureManager.Shapes
{
    public class Rectangle : MyShape
    {
        private static Vector2f DefaultSize = new Vector2f(100, 100);
        public Vector2f Size { get; }

        public Rectangle(Vector2f p1, Vector2f p2) : base(new RectangleShape(new Vector2f(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y))))
        {
            Vector2f center = new Vector2f((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            Size = ((RectangleShape)base.Shape).Size;
            Shape.Position = new Vector2f(center.X - Size.X / 2, center.Y - Size.Y / 2);
            Type = ShapeType.Rectangle;
        }

        public Rectangle(Vector2f center) : base(new RectangleShape(DefaultSize))
        {
            Shape.Position = new Vector2f(center.X - DefaultSize.X / 2, center.Y - DefaultSize.Y / 2);
        }

        public override float GetSquare => Size.X * Size.Y;

        public override float GetPerimeter => Size.X + Size.Y;

        public override object Clone()
        {
            Rectangle clone = new Rectangle(new Vector2f(100, 100));

            ((RectangleShape)clone.Shape).Size = ((RectangleShape)Shape).Size;
            clone.FillColor = FillColor;
            clone.Position = new Vector2f(Position.X, Position.Y);
            clone.OutlineColor = OutlineColor;
            clone.OutlineThickness = OutlineThickness;

            return clone;
        }
    }
}
