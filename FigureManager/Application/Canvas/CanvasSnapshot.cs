using FigureManager.Shapes;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FigureManager.Application.Commands
{
    public class CanvasSnapshot
    {
        private readonly Canvas _canvas;
        private readonly Color _background;
        private readonly List<MyShape> _shapes;

        public CanvasSnapshot(Canvas canvas, Color background, List<MyShape> shapes)
        {
            _canvas = canvas;
            _background = background;
            _shapes = shapes.Select( s => (MyShape)s.Clone()).ToList();
        }

        public void Restore()
        {
            _canvas.Background = _background;
            _canvas.Shapes = _shapes;
            _canvas.SelectedShapes = new List<MyShape>();
        }
    }
}
