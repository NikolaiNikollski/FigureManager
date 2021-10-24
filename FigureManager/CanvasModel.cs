using FigureManager.Figures;
using SFML.Graphics;
using System.Collections.Generic;

namespace FigureManager
{
    public class CanvasModel
    {
        public string Name { get; set; }

        public uint Height { get; set; }

        public uint Width { get; set; }

        public Color Background { get; set; }

        public List<MyShape> Shapes { get; set; }

        public List<MyShape> SelectedShapes { get; set; }
    }
}
