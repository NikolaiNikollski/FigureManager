using SFML.Graphics;
using SFML.System;
using System;

namespace FigureManager.Shapes
{
    public abstract class MyShape : Shape
    {
        protected readonly Shape Shape;

        public MyShape(Shape shape)
        {
            Shape = shape;
        }

        public virtual void Draw(RenderWindow window)
        {
            window.Draw(Shape);
        }

        public new virtual Vector2f Position { get => Shape.Position; set => Shape.Position = value; }

        public new virtual FloatRect GetGlobalBounds()
        {
            return Shape.GetGlobalBounds();
        }

        public new virtual Color FillColor { get => Shape.FillColor; set => Shape.FillColor = value; }

        public new virtual Color OutlineColor { get => Shape.OutlineColor; set => Shape.OutlineColor = value; }

        public new virtual float OutlineThickness { get => Shape.OutlineThickness; set => Shape.OutlineThickness = value; }

        public virtual MyShape GetFrame
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

        public ShapeType Type { get; protected set; }

        public abstract float GetSquare { get; }

        public abstract float GetPerimeter { get; }

        public virtual string GetDescription => string.Format("{0}: P={1}; S={2}", GetType().Name, GetPerimeter, GetSquare);

        public override uint GetPointCount()
        {
            throw new NotImplementedException();
        }

        public override Vector2f GetPoint(uint index)
        {
            throw new NotImplementedException();
        }
    }
}
