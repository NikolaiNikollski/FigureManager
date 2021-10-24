using FigureManager.Application.Commands;
using FigureManager.Shapes;
using FigureManager.ToolBar;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace FigureManager.Application
{
    class Application
    {
        private const int WinWidth = 800;
        private const int WinHeight = 600;
        private Application() { }
        private static Application _instance;

        private static Toolbar _toolBar;
        private static Canvas _canvas;
        private static RenderWindow win;

        private static Stack<CanvasSnapshot> _commandHistory;

        public static Application GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Application();

            }
            return _instance;
        }

        public static void Start(List<MyShape> shapes)
        {
            _commandHistory = new Stack<CanvasSnapshot>();
            _canvas = new Canvas(shapes, _commandHistory);
            _toolBar = new Toolbar(WinWidth, _canvas);

            win = new RenderWindow(new VideoMode(WinWidth, WinHeight), "");
            ListenEvents();

            foreach (MyShape shape in _canvas.Shapes)
            {
                shape.FillColor = Color.Green;
                Console.WriteLine(shape.GetDescription);
            }

            while (win.IsOpen)
            {
                win.DispatchEvents();

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
                _toolBar.MouseLeftPressed(new Vector2f(arguments.X, arguments.Y), Keyboard.IsKeyPressed(Keyboard.Key.LControl));

            }
        }

        private static void Win_MouseButtonReleased(object sender, EventArgs e)
        {
            MouseButtonEventArgs arguments = (MouseButtonEventArgs)e;
            if (arguments.Button == Mouse.Button.Left)
            {
                _canvas.StopDragAndDrope(new Vector2f(arguments.X, arguments.Y));
            }
        }

        private static void Win_KeyPressed(object sender, EventArgs e)
        {
            KeyEventArgs arguments = (KeyEventArgs)e;
            if (arguments.Control && arguments.Code == Keyboard.Key.U)
            {
                _toolBar.CtrlUPressed();
            }
            else if (arguments.Control && arguments.Code == Keyboard.Key.G)
            {
                _toolBar.CtrlGPressed();
            }

            else if (arguments.Control && arguments.Code == Keyboard.Key.Z)
            {
                if (_commandHistory.Count > 0)
                {
                    _commandHistory.Pop().Restore();
                }
            }
        }
    }
}