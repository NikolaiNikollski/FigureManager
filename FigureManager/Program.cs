using FigureManager.Shapes;
using FigureManager.Txt;
using System.Collections.Generic;

namespace FigureManager
{
    class Program
    {
        const string InputFilePath = "Txt/input.txt";
        const string OutputFilePath = "Txt/output.txt";

        static void Main(string[] args)
        {
            List<MyShape> shapes = TxtHelper.LoadShapes(InputFilePath);
            TxtHelper.SetShapeDescription(shapes, OutputFilePath);
            Application.Application.Start(shapes);
        }
    }
}
