using FigureManager.Txt;

namespace FigureManager
{
    class Program
    {
        const string InputFilePath = "Txt/input.txt";
        const string OutputFilePath = "Txt/output.txt";

        static void Main(string[] args)
        {
            CanvasModel canvas = TxtHelper.LoadShapes(InputFilePath);
            TxtHelper.SetShapeDescription(canvas, OutputFilePath);
            Application.Start(canvas);
        }
    }
}
