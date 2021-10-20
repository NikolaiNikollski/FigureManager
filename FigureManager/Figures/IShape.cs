using SFML.Graphics;
using SFML.System;

namespace FigureManager.Figures
{
    public interface IShape
    {
        void Draw(RenderWindow window);

        FloatRect GetGlobalBounds();

        Vector2f Position { get; set; }

        public float OutlineThickness { get; set; }

        public Color OutlineColor { get; set; }

        Color FillColor { get; set; }

        IShape GetFrame { get; }

        // Area 
        float GetSquare { get; }

        float GetPerimeter { get; }

        string GetDescription { get; }
    }
}
