using FigureManager.Figures;
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

        readonly Color ActiveColor = Color.White;
        List<Button> buttons = new List<Button>();
        Rectangle Background;

        public Toolbar(uint canvasWidth)
        {
            Background = new Rectangle(new Vector2f(0, 0), new Vector2f(canvasWidth, ToolBarHeight));
            Background.FillColor = new Color(192, 192, 192);

            buttons.Add(new CustomButton(ButtonType.DragAndDrope, "Img/dragAndDrope.png"));
            buttons.Add(new CustomButton(ButtonType.Fill, "Img/filler.png"));
            buttons.Add(new CustomButton(ButtonType.AddRectangle, "Img/rectangle.png"));
            buttons.Add(new CustomButton(ButtonType.AddTriangle, "Img/triangle.png"));
            buttons.Add(new CustomButton(ButtonType.AddCircle, "Img/circle.png"));
            buttons.Add(new CustomButton(ButtonType.SetOutline0, "Img/zero.png"));
            buttons.Add(new CustomButton(ButtonType.SetOutline1, "Img/one.png"));
            buttons.Add(new CustomButton(ButtonType.SetOutline2, "Img/two.png"));
            buttons.Add(new CustomButton(ButtonType.SetOutline3, "Img/three.png"));
            buttons.Add(new CustomButton(ButtonType.SetOutlineColor, "Img/line.png"));
            buttons.Add(new ColorPickButton(Color.White));
            buttons.Add(new ColorPickButton(Color.Black));
            buttons.Add(new ColorPickButton(Color.Green));
            buttons.Add(new ColorPickButton(Color.Red));
            buttons.Add(new ColorPickButton(Color.Blue));
        }

        public void Draw(RenderWindow win)
        {
            Background.Draw(win);
            buttons
                .Where(b => b.Type == ButtonType.ChooseColor)
                .Select(b => (ColorPickButton)b)
                .First(b => b.Background.FillColor == ActiveColor)
                .FillColor = Color.Magenta;
            int XPosition = XPositionBase;

            foreach (Button button in buttons)
            {
                button.Position = new Vector2f(XPosition, YPosition);
                XPosition += XPositionStep;
                button.Draw(win);
            }
        }
    }
}
