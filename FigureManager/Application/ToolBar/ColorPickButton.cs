using FigureManager.Figures;
using SFML.Graphics;
using SFML.System;

namespace FigureManager.ToolBar
{
    class ColorPickButton : Button
    {
        public Rectangle Background;

        public ColorPickButton(Color color) : base(new Vector2f(0, 0), new Vector2f(ButtonSize, ButtonSize))
        {
            Type = ButtonType.ChooseColor;

            Shape.OutlineThickness = 2;
            Shape.OutlineColor = Color.Black;

            Rectangle sprite = new Rectangle(
            new Vector2f(Shape.Position.X + ButtonPadding, Shape.Position.Y + ButtonPadding),
            new Vector2f(Shape.Position.X + ButtonPadding + BackgroundSize, Shape.Position.Y + ButtonPadding + BackgroundSize));
            sprite.OutlineThickness = 2;
            sprite.OutlineColor = Color.Black;
            sprite.FillColor = color;
            Background = sprite;
        }

        public override void Draw(RenderWindow win)
        {
            win.Draw(Shape);
            Background.Draw(win);
        }

        public override Vector2f Position
        {
            get => Shape.Position;
            set
            {
                Shape.Position = value; Background.Position = new Vector2f(value.X + ButtonPadding, value.Y + ButtonPadding);
            }
        }
    }
}
