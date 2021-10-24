using FigureManager.Figures;
using SFML.System;

namespace FigureManager.ToolBar
{
    public abstract class Button : Rectangle
    {
        public Button(Vector2f p1, Vector2f p2) : base(p1, p2) { }

        protected const int ButtonSize = 36;
        protected const int ButtonPadding = 6;
        protected const int BackgroundSize = 24;

        public ButtonType Type { get; set; }
        public bool IsActive { get; set; }
    }
}
