using FigureManager.ToolBar;
using SFML.Graphics;
using SFML.System;

namespace FigureManager.Application.ToolBar.States
{
    public class DragAndDropeState : State
    {
        public DragAndDropeState(Canvas canvas, Toolbar toolbar, Color activeColor) : base(toolbar, canvas, activeColor) 
        {
            ActiveCustomButton = ButtonType.DragAndDrope;
        }

        public override bool CanvasClick(Vector2f coords, bool isCtrlPressed, Color color)
        {
            Canvas.SelectShape(coords, !isCtrlPressed);
            Canvas.StartDragAndDrope(new Vector2f(coords.X, coords.Y));
            return true;
        }
    }
}
