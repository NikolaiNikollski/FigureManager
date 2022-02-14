using FigureManager.Shapes;
using FigureManager.ToolBar;
using SFML.Graphics;
using SFML.System;

namespace FigureManager.Application.ToolBar.States
{
    public class AddShapeState : MainState
    {
        private ShapeType ShapeType;

        public AddShapeState(Canvas canvas, Toolbar toolbar, ShapeType shapeType, Color activeColor) : base(toolbar, canvas, activeColor)
        {
            ShapeType = shapeType;
            switch (shapeType)
            {
                case ShapeType.Rectangle:
                    ActiveCustomButton = ButtonType.AddRectangle;
                    break;
                case ShapeType.Triangle:
                    ActiveCustomButton = ButtonType.AddTriangle;
                    break;
                case ShapeType.Circle:
                    ActiveCustomButton = ButtonType.AddCircle;
                    break;
            }
        }

        public override bool CanvasClick(Vector2f coords, bool isCtrlPressed, Color color)
        {
            return Canvas.AddShape(coords, ShapeType, color);
        }
    }
}
