using FigureManager.Shapes;
using SFML.Graphics;
using SFML.System;

namespace FigureManager.Application.ImportExportDto
{
    public class TriangleDto
    {
        public Vector2f P1 { get; set; }

        public Vector2f P2 { get; set; }

        public Vector2f P3 { get; set; }

        public Vector2f Position { get; set; }

        public Color ShapeColor { get; set; }

        public float OutlineThickness { get; set; }

        public Color OutlineColor { get; set; }

        public static TriangleDto TriangleExport(Triangle triangle)
        {
            return new TriangleDto
            {
                P1 = triangle.p1,
                P2 = triangle.p2,
                P3 = triangle.p3,
                Position = triangle.Position,
                ShapeColor = triangle.FillColor,
                OutlineThickness = triangle.OutlineThickness,
                OutlineColor = triangle.OutlineColor
            };
        }

        public static Triangle TriangleImport(TriangleDto triangleDto)
        {
            Triangle triangle = new Triangle(triangleDto.P1, triangleDto.P2, triangleDto.P3);
            triangle.FillColor = triangleDto.ShapeColor;
            triangle.OutlineThickness = triangleDto.OutlineThickness;
            triangle.OutlineColor = triangleDto.OutlineColor;
            triangle.Position = triangleDto.Position;

            return triangle;
        }
    }
}
