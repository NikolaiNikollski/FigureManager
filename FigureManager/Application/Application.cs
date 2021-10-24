using FigureManager.Shapes;
using FigureManager.ToolBar;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace FigureManager.Application
{
    class Application
    {
        private Application() { }
        private static Application _instance;

        private static Toolbar _toolBar;
        private static Canvas.CanvasModel _canvas;
        static RenderWindow win;

        public static Application GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Application();

            }
            return _instance;
        }

        public static void Start(Canvas.CanvasModel canvas)
        {
            _canvas = canvas;
            _toolBar = new Toolbar(_canvas.Width, canvas);

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

                _canvas.StartDragAndDrope(new Vector2f(arguments.X, arguments.Y));
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
        }
    }
}