using FigureManager.Shapes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FigureManager.Shapes
{
    public class CompoundShape : MyShape
    {
        public List<MyShape> Shapes { get; }

        private float lX;
        private float rX;
        private float tY;
        private float bY;

        public CompoundShape(List<MyShape> inShapes) : base(inShapes.FirstOrDefault())
        {
            Shapes = inShapes;
            Type = ShapeType.CompoundShape;
        }

        public override MyShape GetFrame
        {
            get
            {
                FloatRect FrameBounds = GetGlobalBounds();
                MyShape frame = new Rectangle(
                    new Vector2f(FrameBounds.Left, FrameBounds.Top),
                    new Vector2f(FrameBounds.Left + FrameBounds.Width, FrameBounds.Top + FrameBounds.Height));
                frame.FillColor = Color.Transparent;
                frame.OutlineColor = Color.Black;
                frame.OutlineThickness = 2;

                return frame;
            }
        }

        public override Color FillColor
        {
            get => Shapes[0].FillColor;
            set
            {
                foreach (MyShape shape in Shapes)
                {
                    shape.FillColor = value;
                }
            }
        }

        public override Color OutlineColor
        {
            get => Shapes[0].FillColor;
            set
            {
                foreach (MyShape shape in Shapes)
                {
                    shape.FillColor = value;
                }
            }
        }

        public override float OutlineThickness
        {
            get => Shapes[0].OutlineThickness;
            set
            {
                foreach (MyShape shape in Shapes)
                {
                    shape.OutlineThickness = value;
                }
            }
        }

        public override Vector2f Position
        {
            get
            {
                return new Vector2f(lX, tY);
            }
            set
            {
                float dX = value.X - lX;
                float dY = value.Y - tY;

                foreach (MyShape shape in Shapes)
                {
                    shape.Position = new Vector2f(shape.Position.X + dX, shape.Position.Y + dY);
                }
            }
        }

        public override void Draw(RenderWindow window)
        {
            foreach (MyShape shape in Shapes)
            {
                shape.Draw(window);
            }
        }

        public override FloatRect GetGlobalBounds()
        {
            FloatRect bounds = Shapes[0].GetGlobalBounds();
            lX = bounds.Left;
            rX = bounds.Left + bounds.Width;
            tY = bounds.Top;
            bY = bounds.Top + bounds.Height;

            for (int i = 1; i < Shapes.Count; i++)
            {
                bounds = Shapes[i].GetGlobalBounds();
                lX = Math.Min(lX, bounds.Left);
                rX = Math.Max(rX, bounds.Left + bounds.Width);
                tY = Math.Min(tY, bounds.Top);
                bY = Math.Max(bY, bounds.Top + bounds.Height);
            }

            return new FloatRect(lX, tY, Math.Abs(lX - rX), Math.Abs(tY - bY));
        }

        public override float GetSquare => Shapes.Select(s => s.GetSquare).Aggregate((x, y) => x + y);

        public override float GetPerimeter => Shapes.Select(s => s.GetPerimeter).Aggregate((x, y) => x + y);

        public override object Clone()
        {
            CompoundShape clone = new CompoundShape(Shapes.Select(s => (MyShape)s.Clone()).ToList());
            return clone;
        }
    }
}
