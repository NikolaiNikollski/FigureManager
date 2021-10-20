using FigureManager.Figures;
using FigureManager.Txt;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FigureManager
{
    class Program
    {
        const string AppName = "Paint";
        const string InputFilePath = "input.txt";
        const string OutputFilePath = "output.txt";

        static RenderWindow win;

        static bool isMove = false;
        static MyShape movingShape;
        static float dX;
        static float dY;
        static string message;

        static List<MyShape> shapes;
        static List<MyShape> selectedShapes = new List<MyShape>();
        static Vector2i mousePosition;

        static void Main(string[] args)
        {
            win = new RenderWindow(new VideoMode(800, 600), AppName);
            ListenEvents();

            shapes = TxtHelper.LoadShapes(InputFilePath);
            TxtHelper.SetShapeDescription(shapes, OutputFilePath);

            foreach (MyShape shape in shapes)
            {
                shape.FillColor = Color.Green;
                Console.WriteLine(shape.GetDescription);
            }

            while (win.IsOpen)
            {
                mousePosition = Mouse.GetPosition();


                if (!string.IsNullOrEmpty(message))
                {
                    Console.WriteLine(message);
                    message = null;
                }

                win.DispatchEvents();
                win.Clear(Color.White);

                if (isMove)
                {
                    movingShape.FillColor = Color.Red;
                    movingShape.Position = new Vector2f(mousePosition.X - dX, mousePosition.Y - dY);
                    movingShape.GetFrame.Position = movingShape.Position;
                }

                foreach (MyShape shape in shapes.Where(s => !selectedShapes.Contains(s)))
                {
                    shape.Draw(win);
                }

                foreach (MyShape selectedShape in selectedShapes)
                {
                    selectedShape.Draw(win);
                    selectedShape.GetFrame.Draw(win);
                }

                win.Display();
            }
        }

        private static void ListenEvents()
        {
            win.Closed += Win_Closed;
            win.MouseButtonPressed += Win_MouseButtonPressed;
            win.MouseButtonReleased += Win_MouseButtonReleased;
            win.KeyPressed += Win_KeyPressed;
        }

        private static void Win_Closed(object sender, EventArgs e)
        {
            win.Close();
        }

        private static void Win_MouseButtonPressed(object sender, EventArgs e)
        {
            MouseButtonEventArgs arguments = (MouseButtonEventArgs)e;
            if (arguments.Button == Mouse.Button.Left)
            {
                movingShape = shapes.FirstOrDefault(s => s.GetGlobalBounds().Contains(arguments.X, arguments.Y));

                if (movingShape != null)
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.LControl))
                    {
                        if (!selectedShapes.Contains(movingShape))
                        {
                            selectedShapes.Add(movingShape);
                        }
                    }
                    else
                    {
                        selectedShapes.Clear();
                        selectedShapes.Add(movingShape);
                    }

                    shapes.Remove(movingShape);
                    shapes.Insert(0, movingShape);
                    isMove = true;
                    dX = mousePosition.X - movingShape.Position.X;
                    dY = mousePosition.Y - movingShape.Position.Y;
                    movingShape.Position = new Vector2f(movingShape.Position.X + dX, movingShape.Position.Y + dY);
                }
            }
        }

        private static void Win_MouseButtonReleased(object sender, EventArgs e)
        {
            MouseButtonEventArgs arguments = (MouseButtonEventArgs)e;
            if (arguments.Button == Mouse.Button.Left && isMove)
            {
                movingShape.FillColor = Color.Green;
                isMove = false;
            }
        }

        private static void Win_KeyPressed(object sender, EventArgs e)
        {
            KeyEventArgs arguments = (KeyEventArgs)e;
            if (arguments.Control && arguments.Code == Keyboard.Key.U && selectedShapes.Count >= 2)
            {
                CompoundShape compoundShape = new CompoundShape(selectedShapes);
                shapes.Add(compoundShape);
                shapes = shapes.Except(selectedShapes).ToList();
                selectedShapes = new List<MyShape>();
                selectedShapes.Add(compoundShape);
            }
            else if (arguments.Control && arguments.Code == Keyboard.Key.G)
            {
                foreach (MyShape shape in selectedShapes)
                {
                    List<CompoundShape> compoundShapes = selectedShapes.Select(s => s as CompoundShape).Where(s => s != null).ToList();
                    foreach (CompoundShape compoundShape in compoundShapes)
                    {
                        shapes.AddRange(compoundShape.Shapes);
                    }
                    selectedShapes = selectedShapes.Except(compoundShapes.Select(s => (MyShape)s)).ToList();
                    shapes = shapes.Except(compoundShapes.Select(s => (MyShape)s)).ToList();
                }
            }
        }
    }
}
