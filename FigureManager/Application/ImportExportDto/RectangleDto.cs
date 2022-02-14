using FigureManager.Shapes;
using SFML.Graphics;
using SFML.System;

namespace FigureManager.Application.ImportExportDto
{
    public class RectangleDto
    {
        public Vector2f P1 { get; set; }

        public Vector2f P2 { get; set; }

        public Color ShapeColor { get; set; }

        public float OutlineThickness { get; set; }

        public Color OutlineColor { get; set; }

        public static RectangleDto RectangleExport(Rectangle rectangle)
        {
            return new RectangleDto
            {
                P1 = new Vector2f(rectangle.Position.X, rectangle.Position.Y),
                P2 = new Vector2f(rectangle.Position.X + rectangle.Size.X, rectangle.Position.Y + rectangle.Size.Y),
                ShapeColor = ((MyShape)rectangle).FillColor,
                OutlineThickness = rectangle.OutlineThickness,
                OutlineColor = rectangle.OutlineColor
            };
        }

        public static Rectangle RectangleImport(RectangleDto rectangleDto)
        {
            Rectangle rectangle = new Rectangle(rectangleDto.P1, rectangleDto.P2);
            rectangle.FillColor = rectangleDto.ShapeColor;
            rectangle.OutlineThickness = rectangleDto.OutlineThickness;
            rectangle.OutlineColor = rectangleDto.OutlineColor;

            return rectangle;
        }
    }
}
