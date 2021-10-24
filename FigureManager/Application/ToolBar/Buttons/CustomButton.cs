using SFML.Graphics;
using SFML.System;

namespace FigureManager.ToolBar
{
    public class CustomButton : Button
    {
        private Sprite Background;

        public CustomButton(ButtonType type, string imagePath) : base(new Vector2f(0, 0), new Vector2f(ButtonSize, ButtonSize))
        {
            Type = type;

            Shape.OutlineThickness = 2;
            Shape.OutlineColor = Color.Black;

            Sprite sprite = new Sprite(new Texture(imagePath));
            sprite.Position = new Vector2f(Shape.Position.X + ButtonPadding, Shape.Position.Y + ButtonPadding);
            Background = sprite;
        }

        public override void Draw(RenderWindow win)
        {
            win.Draw(Shape);
            win.Draw(Background);
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
