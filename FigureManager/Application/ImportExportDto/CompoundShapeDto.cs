using FigureManager.Shapes;
using System.Collections.Generic;
using System.Linq;

namespace FigureManager.Application.ImportExportDto
{
    public class CompoundShapeDto
    {
        public List<CircleDto> Circles { get; set; }

        public List<RectangleDto> Rectangles { get; set; }

        public List<TriangleDto> Triangles { get; set; }

        public List<CompoundShapeDto> Compounds { get; set; }

        public static CompoundShapeDto CompoundExport(CompoundShape compounds)
        {
            return new CompoundShapeDto
            {
                Circles = compounds.Shapes.Where(s => s.Type == ShapeType.Circle).Select(s => CircleDto.CircleExport(s as Circle)).ToList(),
                Rectangles = compounds.Shapes.Where(s => s.Type == ShapeType.Rectangle).Select(s => RectangleDto.RectangleExport(s as Rectangle)).ToList(),
                Triangles = compounds.Shapes.Where(s => s.Type == ShapeType.Triangle).Select(s => TriangleDto.TriangleExport(s as Triangle)).ToList(),
                Compounds = compounds.Shapes.Where(s => s.Type == ShapeType.CompoundShape).Select(s => CompoundShapeDto.CompoundExport(s as CompoundShape)).ToList(),
            };
        }

        public static CompoundShape CompoundShapeImport(CompoundShapeDto compoundShapeDto)
        {
            List<MyShape> shapes = new List<MyShape>();
            shapes.AddRange(compoundShapeDto.Circles.Select(s => CircleDto.CircleImport(s)));
            shapes.AddRange(compoundShapeDto.Rectangles.Select(s => RectangleDto.RectangleImport(s)));
            shapes.AddRange(compoundShapeDto.Triangles.Select(s => TriangleDto.TriangleImport(s)));
            shapes.AddRange(compoundShapeDto.Compounds.Select(s => CompoundShapeDto.CompoundShapeImport(s)));

            return new CompoundShape(shapes);
        }
    }
}
