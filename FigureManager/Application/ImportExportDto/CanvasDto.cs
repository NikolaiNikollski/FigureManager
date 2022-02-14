using FigureManager.Shapes;
using SFML.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace FigureManager.Application.ImportExportDto
{
    public class CanvasDto
    {
        public Color BackgroundColor { get; set; }

        public List<CircleDto> Circles { get; set; }

        public List<RectangleDto> Rectangles { get; set; }

        public List<TriangleDto> Triangles { get; set; }

        public List<CompoundShapeDto> Compounds { get; set; }

        public static CanvasDto CanvasExport(Canvas canvas)
        {
            return new CanvasDto
            {
                BackgroundColor = canvas.Background,
                Circles = canvas.Shapes.Where(s => s.Type == ShapeType.Circle).Select(s => CircleDto.CircleExport(s as Circle)).ToList(),
                Rectangles = canvas.Shapes.Where(s => s.Type == ShapeType.Rectangle).Select(s => RectangleDto.RectangleExport(s as Rectangle)).ToList(),
                Triangles = canvas.Shapes.Where(s => s.Type == ShapeType.Triangle).Select(s => TriangleDto.TriangleExport(s as Triangle)).ToList(),
                Compounds = canvas.Shapes.Where(s => s.Type == ShapeType.CompoundShape).Select(s => CompoundShapeDto.CompoundExport(s as CompoundShape)).ToList(),
            };
        }

        public static Canvas CanvasImport(CanvasDto canvasDto)
        {
            List<MyShape> shapes = new List<MyShape>();
            shapes.AddRange(canvasDto.Circles.Select(s => CircleDto.CircleImport(s)));
            shapes.AddRange(canvasDto.Rectangles.Select(s => RectangleDto.RectangleImport(s)));
            shapes.AddRange(canvasDto.Triangles.Select(s => TriangleDto.TriangleImport(s)));
            shapes.AddRange(canvasDto.Compounds.Select(s => CompoundShapeDto.CompoundShapeImport(s)));

            Canvas canvas = new Canvas(shapes, new Stack<Commands.CanvasSnapshot>());
            canvas.Background = canvasDto.BackgroundColor;
            return canvas;
        }
    }
}
