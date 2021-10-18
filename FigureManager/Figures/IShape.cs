using SFML.Graphics;

namespace FigureManager.Figures 
{
    public interface IShape
    {
        public void Draw(RenderWindow window);

        public float GetSquare { get; }

        public float GetPerimeter { get; }

        public string GetDescription { get; }

        public Color FillColor { get; set; }
    }
}
