using FigureManager.Canvas;
using FigureManager.Figures;
using FigureManager.ToolBar;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FigureManager
{
    class Application
    {
        private Application() { }
        private static Application _instance;

        static Vector2i mousePosition;
        static bool isMove = false;
        static MyShape movingShape;
        static float dX;
        static float dY;

        private static Toolbar _toolBar;
        private static CanvasH _canvas;
        static RenderWindow win;


        public static Application GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Application();
                
            }
            return _instance;
        }

        public static void Start(CanvasH canvas)
        {
            _canvas = canvas;
            _toolBar = new Toolbar(_canvas.Width);

            win = new RenderWindow(new VideoMode(_canvas.Width, _canvas.Height), _canvas.Name);
            ListenEvents();

            foreach (MyShape shape in _canvas.Shapes)
            {
                shape.FillColor = Color.Green;
                Console.WriteLine(shape.GetDescription);
            }

            while (win.IsOpen)
            {
                win.DispatchEvents();

                mousePosition = Mouse.GetPosition();


                if (isMove)
                {
                    movingShape.FillColor = Color.Red;
                    movingShape.Position = new Vector2f(mousePosition.X - dX, mousePosition.Y - dY);
                    movingShape.GetFrame.Position = movingShape.Position;
                }

                

                _canvas.Draw(win);
                _toolBar.Draw(win);

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
                movingShape = _canvas.Shapes.FirstOrDefault(s => s.GetGlobalBounds().Contains(arguments.X, arguments.Y));

                if (movingShape != null)
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.LControl))
                    {
                        if (!_canvas.SelectedShapes.Contains(movingShape))
                        {
                            _canvas.SelectedShapes.Add(movingShape);
                        }
                    }
                    else
                    {
                        _canvas.SelectedShapes.Clear();
                        _canvas.SelectedShapes.Add(movingShape);
                    }

                    _canvas.Shapes.Remove(movingShape);
                    _canvas.Shapes.Insert(0, movingShape);
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
            if (arguments.Control && arguments.Code == Keyboard.Key.U && _canvas.SelectedShapes.Count >= 2)
            {
                CompoundShape compoundShape = new CompoundShape(_canvas.SelectedShapes);
                _canvas.Shapes.Add(compoundShape);
                _canvas.Shapes = _canvas.Shapes.Except(_canvas.SelectedShapes).ToList();
                _canvas.SelectedShapes = new List<MyShape>();
                _canvas.SelectedShapes.Add(compoundShape);
            }
            else if (arguments.Control && arguments.Code == Keyboard.Key.G)
            {
                foreach (MyShape shape in _canvas.SelectedShapes)
                {
                    List<CompoundShape> compoundShapes = _canvas.SelectedShapes.Select(s => s as CompoundShape).Where(s => s != null).ToList();
                    foreach (CompoundShape compoundShape in compoundShapes)
                    {
                        _canvas.Shapes.AddRange(compoundShape.Shapes);
                    }
                    _canvas.SelectedShapes = _canvas.SelectedShapes.Except(compoundShapes.Select(s => (MyShape)s)).ToList();
                    _canvas.Shapes = _canvas.Shapes.Except(compoundShapes.Select(s => (MyShape)s)).ToList();
                }
            }
        }
    }
}