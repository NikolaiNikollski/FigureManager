using FigureManager.Canvas;
using FigureManager.Shapes;
using FigureManager.ToolBar;
using SFML.Graphics;
using SFML.System;

namespace FigureManager.Application.ToolBar.States
{
    public class DragAndDropeState : State
    {
        public DragAndDropeState(CanvasModel canvas, Toolbar toolbar, Color activeColor) : base(toolbar, canvas, activeColor) 
        {
            ActiveCustomButton = ButtonType.DragAndDrope;
        }

        public override void CanvasClick(Vector2f coords, bool isCtrlPressed, Color color)
        {
            Canvas.SelectShape(coords, !isCtrlPressed);
        }
    }
}
