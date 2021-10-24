using FigureManager.Shapes;
using FigureManager.ToolBar;
using SFML.Graphics;
using SFML.System;
using System.Linq;

namespace FigureManager.Application.ToolBar.States
{
    public abstract class State
    {
        protected Canvas Canvas;
        protected Toolbar Toolbar;
        public ButtonType ActiveCustomButton { get; protected set; }
        public Color ActiveColor = Color.White;

        protected State(Toolbar toolbar, Canvas canvas, Color activeColor)
        {
            Toolbar = toolbar;
            Canvas = canvas;
            ActiveColor = activeColor;
        }

        public abstract bool CanvasClick(Vector2f coords, bool isCtrlPressed, Color color);

        public void MouseLeftClick(Vector2f coords, bool isCtrlPressed)
        {
            if (Toolbar.Background.GetGlobalBounds().Contains(coords.X, coords.Y))
            {
                Button button = Toolbar.Buttons.FirstOrDefault(s => s.GetGlobalBounds().Contains(coords.X, coords.Y));
                if (button != null)
                {
                    switch (button.Type)
                    {
                        case ButtonType.DragAndDrope:
                            Toolbar.SetState(new DragAndDropeState(Canvas, Toolbar, ActiveColor));
                            break;
                        case ButtonType.Fill:
                            Toolbar.SetState(new FillState(Canvas, Toolbar, ActiveColor));
                            break;
                        case ButtonType.AddRectangle:
                            Toolbar.SetState(new AddShapeState(Canvas, Toolbar, ShapeType.Rectangle, ActiveColor));
                            break;
                        case ButtonType.AddTriangle:
                            Toolbar.SetState(new AddShapeState(Canvas, Toolbar, ShapeType.Triangle, ActiveColor));
                            break;
                        case ButtonType.AddCircle:
                            Toolbar.SetState(new AddShapeState(Canvas, Toolbar, ShapeType.Circle, ActiveColor));
                            break;
                        case ButtonType.ChooseColor:
                            ActiveColor = ((ColorPickButton)button).Color;
                            break;
                        case ButtonType.SetOutline0:
                            Canvas.SetOutlineThickness(0);
                            break;
                        case ButtonType.SetOutline1:
                            Canvas.SetOutlineThickness(2);
                            break;
                        case ButtonType.SetOutline2:
                            Canvas.SetOutlineThickness(4);
                            break;
                        case ButtonType.SetOutline3:
                            Canvas.SetOutlineThickness(6);
                            break;
                        case ButtonType.SetOutlineColor:
                            Canvas.SetOutlineColor(ActiveColor);
                            break;
                    }
                }
            }
            else
            {
                bool result = CanvasClick(coords, isCtrlPressed, ActiveColor);               
            }
        }
    }
}
