using FigureManager.Canvas;
using FigureManager.Shapes;
using SFML.System;

namespace FigureManager.Commands
{
    class AddShape : ICommand
    {
        private readonly Canvas.CanvasModel _canvas;
        private readonly MyShape _shape;

        AddShape(Canvas.CanvasModel canvas, ShapeType type, Vector2f coords)
        {
            switch (type)
            {
                case ShapeType.Rectangle:
                    _shape = new Rectangle(coords);
                    break;
                case ShapeType.Triangle:
                    _shape = new Triangle(coords);
                    break;
                case ShapeType.Circle:
                    _shape = new Circle(coords);
                    break;
            }
            _canvas = canvas;
        }

        public bool Execute()
        {
            _canvas.Shapes.Add(_shape);
            return true;
        }
    }
}
