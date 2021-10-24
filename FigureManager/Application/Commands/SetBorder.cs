using FigureManager.Canvas;
using FigureManager.Shapes;
using SFML.System;

namespace FigureManager.Commands
{
    class SetBorder : ICommand
    {
        private readonly Canvas.Canvas _canvas;
        private readonly MyShape _shape;

        SetBorder(Canvas.Canvas canvas, Vector2f coords)
        {
            
        }

        public void Execute()
        {
            _canvas.Shapes.Add(_shape);
        }

        bool ICommand.Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
