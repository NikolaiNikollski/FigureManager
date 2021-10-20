using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FigureManager.Figures
{
    public class CompoundShape : IShape
    {
        public List<IShape> Shapes { get; }

        private float lX;
        private float rX;
        private float tY;
        private float bY;

        public CompoundShape(List<IShape> inShapes)
        {
            Shapes = inShapes;
        }

        public IShape Parent { get; set; }


        public IShape GetFrame
        {
            get
            {
                FloatRect FrameBounds = GetGlobalBounds();
                IShape frame =  new Rectangle(
                    new Vector2f(FrameBounds.Left, FrameBounds.Top),
                    new Vector2f(FrameBounds.Left + FrameBounds.Width, FrameBounds.Top + FrameBounds.Height));
                frame.FillColor = Color.Transparent;
                frame.OutlineColor = Color.Black;
                frame.OutlineThickness = 2;

                return frame;
            }
        }

        public Color FillColor
        {
            get => Shapes[0].FillColor;
            set
            {
                foreach (IShape shape in Shapes)
                {
                    shape.FillColor = value;
                }
            }
        }

        public Color OutlineColor
        {
            get => Shapes[0].FillColor;
            set
            {
                foreach (IShape shape in Shapes)
                {
                    shape.FillColor = value;
                }
            }
        }

        public float OutlineThickness
        {
            get => Shapes[0].OutlineThickness;
            set
            {
                foreach (IShape shape in Shapes)
                {
                    shape.OutlineThickness = value;
                }
            }
        }

        public Vector2f Position
        {
            get
            {
                return new Vector2f(lX, tY);
            }
            set
            {
                float dX = value.X - lX;
                float dY = value.Y - tY;

                foreach (IShape shape in Shapes)
                {
                    shape.Position = new Vector2f(shape.Position.X + dX, shape.Position.Y + dY);
                }
            }
        }

        public void Draw(RenderWindow window)
        {
            foreach (IShape shape in Shapes)
            {
                shape.Draw(window);
            }
        }

        public FloatRect GetGlobalBounds()
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

        public float GetSquare => Shapes.Select(s => s.GetSquare).Aggregate((x, y) => x + y);

        public float GetPerimeter => Shapes.Select(s => s.GetPerimeter).Aggregate((x, y) => x + y);

        public string GetDescription => string.Format("{0}: P={1}; S={2}", "Circle", GetPerimeter, GetSquare);
    }
}
