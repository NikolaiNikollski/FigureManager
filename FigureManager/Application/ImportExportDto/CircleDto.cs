using FigureManager.Shapes;
using SFML.Graphics;
using SFML.System;

namespace FigureManager.Application.ImportExportDto
{
    public class CircleDto
    {
        public Vector2f P1 { get; set; }

        public float Radius { get; set; }

        public Color ShapeColor { get; set; }

        public float OutlineThickness { get; set; }

        public Color OutlineColor { get; set; }

        public static CircleDto CircleExport(Circle circle)
        {
            return new CircleDto
            {
                P1 = circle.Position,
                Radius = circle.GetRadius,
                ShapeColor = circle.FillColor,
                OutlineThickness = circle.OutlineThickness,
                OutlineColor = circle.OutlineColor
            };
        }

        public static Circle CircleImport(CircleDto circleDto)
        {
            Circle circle = new Circle(new Vector2f(circleDto.P1.X + circleDto.Radius, circleDto.P1.Y + circleDto.Radius), circleDto.Radius);
            circle.FillColor = circleDto.ShapeColor;
            circle.OutlineThickness = circleDto.OutlineThickness;
            circle.OutlineColor = circleDto.OutlineColor;

            return circle;
        }
    }
}
