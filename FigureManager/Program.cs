using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace FigureManager
{
    class Program
    {
        const string AppName = "Paint";
        const string InputFilePath = "input.txt";
        const string OutputFilePath = "output.txt";

        static RenderWindow win;
        static void Main(string[] args)
        {
            win = new RenderWindow(new SFML.Window.VideoMode(800, 600), AppName);
            win.Closed += Win_Closed;

            List<Shape> shapes = TxtHelper.LoadShapes();
            TxtHelper.SetShapeDescription(shapes);

            while (win.IsOpen)
            {
                win.DispatchEvents();
                win.Clear(Color.Black);

                foreach (Shape shape in shapes)
                {
                    shape.FillColor = Color.Green;
                    win.Draw(shape);
                }

                win.Display();
            }
        }

        private static void Win_Closed(object sender, EventArgs e)
        {
            win.Close();
        }
    }
}
