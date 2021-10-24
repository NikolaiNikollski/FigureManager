using FigureManager.ToolBar;
using SFML.Graphics;
using SFML.System;

namespace FigureManager.Application.ToolBar.States
{
    public class FillState : State
    {
        public FillState(Canvas canvas, Toolbar toolbar, Color activeColor) : base(toolbar, canvas, activeColor)
        {
            ActiveCustomButton = ButtonType.Fill;
        }

        public override bool CanvasClick(Vector2f coords, bool isCtrlPressed, Color color)
        {
           return Canvas.Fill(coords, color);
        }
    }
}
