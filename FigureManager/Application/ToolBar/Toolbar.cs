using FigureManager.Application.Commands;
using FigureManager.Application.ToolBar.States;
using FigureManager.Shapes;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Linq;

namespace FigureManager.ToolBar
{
    public class Toolbar
    {
        private State State;
        private Canvas Canvas;
        public List<Button> Buttons = new List<Button>();
        public Rectangle Background;

        private const int ToolBarHeight = 50;
        private const int YPosition = 7;
        private const int XPositionBase = 5;
        private const int XPositionStep = 45;

        public Toolbar(uint canvasWidth, Canvas canvas)
        {
            Canvas = canvas;
            State = new DragAndDropeState(canvas, this, Color.White);

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
            foreach (Button button in Buttons)
            {
                button.FillColor = Color.White;
            }
            Buttons
                .Where(b => b.Type == ButtonType.ChooseColor)
                .Select(b => (ColorPickButton)b)
                .First(b => b.Color == State.ActiveColor)
                .FillColor = Color.Magenta;
            Buttons
                .First(b => b.Type == State.ActiveCustomButton)
                .FillColor = Color.Magenta;

            Background.Draw(win);
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
            State.MouseLeftClick(coords, isCtrlPressed);
        }

        public void SetState(State state)
        {
            State = state;
        }

        public void CtrlUPressed()
        {
            Canvas.CombineShape();
        }

        public void CtrlGPressed()
        {
            Canvas.DisbandShape();
        }
    }
}
