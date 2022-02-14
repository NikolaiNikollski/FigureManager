using FigureManager.ToolBar;
using SFML.System;
using System.Linq;

namespace FigureManager.Application.ToolBar.States
{
    public abstract class InputOutputState
    {
        protected const string InputPath = "Txt/input.txt";
        protected const string OutputPath = "Txt/output.txt";
        protected Canvas Canvas;
        protected Toolbar Toolbar;
        public ButtonType ActiveInputOutputButton { get; protected set; }

        protected InputOutputState(Toolbar toolbar, Canvas canvas)
        {
            Toolbar = toolbar;
            Canvas = canvas;
        }

        public abstract Canvas Import();

        public abstract void Export(Canvas canvas);

        public void MouseLeftClick(Vector2f coords, bool isCtrlPressed)
        {
            if (Toolbar.Background.GetGlobalBounds().Contains(coords.X, coords.Y))
            {
                Button button = Toolbar.Buttons.FirstOrDefault(s => s.GetGlobalBounds().Contains(coords.X, coords.Y));
                if (button != null)
                {
                    switch (button.Type)
                    {
                        case ButtonType.BaseInputOutput:
                            Toolbar.SetInputOutputState(new BaseInputOutputState(Canvas, Toolbar));
                            break;
                        case ButtonType.PerfectInputOutput:
                            Toolbar.SetInputOutputState(new PerfectInputOutputState(Canvas, Toolbar));
                            break;
                        case ButtonType.Import:
                            Toolbar.Import();
                            break;
                        case ButtonType.Export:
                            Toolbar.Export();
                            break;
                    }
                }
            }
        }
    }
}
