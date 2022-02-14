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
        private MainState MainState;
        private InputOutputState InputOutputState;
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
            MainState = new DragAndDropeState(canvas, this, Color.White);
            InputOutputState = new BaseInputOutputState(canvas, this);

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
            Buttons.Add(new CustomButton(ButtonType.BaseInputOutput, "Img/b.png"));
            Buttons.Add(new CustomButton(ButtonType.PerfectInputOutput, "Img/p.png"));
            Buttons.Add(new CustomButton(ButtonType.Import, "Img/import.png"));
            Buttons.Add(new CustomButton(ButtonType.Export, "Img/export.png"));
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
                .First(b => b.Color == MainState.ActiveColor)
                .FillColor = Color.Magenta;
            Buttons
                .First(b => b.Type == MainState.ActiveCustomButton)
                .FillColor = Color.Magenta;
            Buttons
                .First(b => b.Type == InputOutputState.ActiveInputOutputButton)
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
            MainState.MouseLeftClick(coords, isCtrlPressed);
            InputOutputState.MouseLeftClick(coords, isCtrlPressed);
        }

        public void Import()
        {
            Canvas newCanvas = InputOutputState.Import();
            Canvas.Background = newCanvas.Background;
            Canvas.Shapes = newCanvas.Shapes;
            Canvas.SelectedShapes = new List<MyShape>();
        }

        public void Export()
        {
            InputOutputState.Export(Canvas);
        }

        public void SetInputOutputState(InputOutputState state)
        {
            InputOutputState = state;
        }

        public void SetMainState(MainState state)
        {
            MainState = state;
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
