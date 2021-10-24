using FigureManager.Canvas;
using FigureManager.Shapes;
using FigureManager.ToolBar;
using SFML.Graphics;
using SFML.System;

namespace FigureManager.Application.ToolBar.States
{
    public class FillState : State
    {
        public FillState(CanvasModel canvas, Toolbar toolbar, Color activeColor) : base(toolbar, canvas, activeColor)
        {
            ActiveCustomButton = ButtonType.Fill;
        }

        public override void CanvasClick(Vector2f coords, bool isCtrlPressed, Color color)
        {
            Canvas.Fill(coords, color);
        }
    }
}
