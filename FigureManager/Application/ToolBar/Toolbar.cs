using FigureManager.Shapes;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Linq;

namespace FigureManager.ToolBar
{
    public class Toolbar
    {
        private const int ToolBarHeight = 50;
        private const int YPosition = 7;
        private const int XPositionBase = 5;
        private const int XPositionStep = 45;

        private Color ActiveColor = Color.White;
        private ButtonType ActiveCustomButton = ButtonType.DragAndDrope;
        List<Button> Buttons = new List<Button>();
        Rectangle Background;

        Canvas.Canvas _canvas;//

        public Toolbar(uint canvasWidth, Canvas.Canvas canvas)
        {
            _canvas = canvas;
            Background = new Rectangle(new Vector2f(0, 0), new Vector2f(canvasWidth, ToolBarHeight));
            Background.FillColor = new Color(192, 192, 192);

            Buttons.Add(new CustomButton(ButtonType.DragAndDrope, "Img/dragAndDrope.png"));
            Buttons.Add(new CustomButton(ButtonType.Fill, "Img/filler.png"));
            Buttons.Add(new CustomButton(ButtonType.AddRectangle, "Img/rectangle.png"));
            Buttons.Add(new CustomButton(ButtonType.AddTriangle, "Img/triangle.png"));
            Buttons.Add(new CustomButton(ButtonType.AddCircle, "Img/circle.png"));
            Buttons.Add(new CustomButton(ButtonType.SetOutline0, "Img/zero.png"));
            Buttons.Add(new CustomButton(ButtonType.SetOutline1, "Img/one.png"));
            Buttons.Add(new CustomButton(ButtonType.SetOutline2, "Img/two.png"));
            Buttons.Add(new CustomButton(ButtonType.SetOutline3, "Img/three.png"));
            Buttons.Add(new CustomButton(ButtonType.SetOutlineColor, "Img/line.png"));
            Buttons.Add(new ColorPickButton(Color.White));
            Buttons.Add(new ColorPickButton(Color.Black));
            Buttons.Add(new ColorPickButton(Color.Green));
            Buttons.Add(new ColorPickButton(Color.Red));
            Buttons.Add(new ColorPickButton(Color.Blue));
        }

        public void Draw(RenderWindow win)
        {
            Background.Draw(win);
            Buttons
                .Where(b => b.Type == ButtonType.ChooseColor)
                .Select(b => (ColorPickButton)b)
                .First(b => b.Color == ActiveColor)
                .FillColor = Color.Magenta;
            Buttons
                .First(b => b.Type == ActiveCustomButton)
                .FillColor = Color.Magenta;

            int XPosition = XPositionBase;
            foreach (Button button in Buttons)
            {
                button.Position = new Vector2f(XPosition, YPosition);
                XPosition += XPositionStep;
                button.Draw(win);
            }
        }

        public void MouseLeftPressed(Vector2f coords, bool isCtrlPressed)
        {
            if (Background.GetGlobalBounds().Contains(coords.X, coords.Y))
            {
                Button button = GetButton(coords);
                if (button != null)
                {
                    switch (button.Type)
                    {
                        case ButtonType.DragAndDrope:
                        case ButtonType.Fill:
                        case ButtonType.AddRectangle:
                        case ButtonType.AddTriangle:
                        case ButtonType.AddCircle:
                            ActiveCustomButton = button.Type;
                            break;
                        case ButtonType.ChooseColor:
                            ActiveColor = ((ColorPickButton)button).Color;
                            break;
                        case ButtonType.SetOutline0:
                            _canvas.SetOutlineThickness(0);
                            break;
                        case ButtonType.SetOutline1:
                            _canvas.SetOutlineThickness(1);
                            break;
                        case ButtonType.SetOutline2:
                            _canvas.SetOutlineThickness(2);
                            break;
                        case ButtonType.SetOutline3:
                            _canvas.SetOutlineThickness(3);
                            break;
                        case ButtonType.SetOutlineColor:
                            _canvas.SetOutlineColor(ActiveColor);
                            break;
                    }
                }
            }
            else
            {
                switch (ActiveCustomButton)
                {
                    case ButtonType.DragAndDrope:
                        _canvas.SelectShape(coords, !isCtrlPressed);
                        break;
                    case ButtonType.Fill:
                        _canvas.Fill(coords, ActiveColor);
                        break;
                    case ButtonType.AddRectangle:
                        _canvas.AddShape(coords, ShapeType.Rectangle);
                        break;
                    case ButtonType.AddTriangle:
                        _canvas.AddShape(coords, ShapeType.Triangle);
                        break;
                    case ButtonType.AddCircle:
                        _canvas.AddShape(coords, ShapeType.Circle);
                        break;
                }
            }
        }

        private Button GetButton(Vector2f coords)
        {
            return Buttons.FirstOrDefault(s => s.GetGlobalBounds().Contains(coords.X, coords.Y));
        }
    }
}
