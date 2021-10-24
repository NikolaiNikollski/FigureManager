using FigureManager.Figures;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FigureManager.Canvas
{
    public class CanvasH
    {
        public string Name { get; set; }

        public uint Height { get; set; }

        public uint Width { get; set; }

        public Color Background { get; set; }

        public List<MyShape> Shapes { get; set; }

        public List<MyShape> SelectedShapes { get; set; }


        public void Draw(RenderWindow win)
        {
            win.Clear(Background);
            foreach (MyShape shape in Shapes.Where(s => !SelectedShapes.Contains(s)))
            {
                shape.Draw(win);
            }

            foreach (MyShape selectedShape in SelectedShapes)
            {
                selectedShape.Draw(win);
                selectedShape.GetFrame.Draw(win);
            }
        }
    }
}
