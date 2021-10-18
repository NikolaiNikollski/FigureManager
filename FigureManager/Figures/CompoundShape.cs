using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace FigureManager.Figures
{
    public class CompoundShape: IShape
    {
        List<IShape> shapes = new List<IShape>();

        public string GetDescription => "no";

        public float GetSquare => throw new NotImplementedException();

        public float GetPerimeter => throw new NotImplementedException();

        public Color FillColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Draw(RenderWindow window)
        {
            foreach (IShape shape in shapes)
            {
                shape.Draw(window);
            }
        }
    }
}
