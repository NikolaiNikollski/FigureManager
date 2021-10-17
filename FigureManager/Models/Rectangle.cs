﻿using SFML.Graphics;
using SFML.System;
using System;

namespace FigureManager.Decorator
{
    class Rectangle : RectangleShape, IMathCalculable
    {
        public Rectangle(Vector2f p1, Vector2f p2) : base(new Vector2f(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y)))
        {
            Vector2f center = new Vector2f((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            Position = new Vector2f(center.X - Size.X / 2, center.Y - Size.Y / 2);
        }

        public float Square => Size.X * Size.Y;

        public float Perimeter => Size.X + Size.Y;

        public string GetDescription()
        {
            return string.Format("{0}: P={1}; S={2}", "Rectangle", Perimeter, Square);
        }
    }
}
