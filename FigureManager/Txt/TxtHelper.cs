using FigureManager.Canvas;
using FigureManager.Shapes;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FigureManager.Txt
{
    public static class TxtHelper
    {
        private const string TriangleTypeName = "TRIANGLE";
        private const string RectangleTypeName = "RECTANGLE";
        private const string CircleTypeName = "CIRCLE";
        private const string TypeRegEx = "(.*?):";
        private const string ParametersRegEx = "=(.*?)(;|$)";

        public static Canvas.CanvasModel LoadShapes(string inputPath)
        {
            List<MyShape> shapes = new List<MyShape>();

            StreamReader sr = new StreamReader(inputPath, Encoding.Default);
            string line;
            while ((line = sr.ReadLine()?.Trim()) != null)
            {
                Match typeMatch = Regex.Match(line, TypeRegEx);
                MatchCollection parametersMatches = Regex.Matches(line, ParametersRegEx);
                string type = typeMatch.Groups[1].Value;

                switch (type)
                {
                    case TriangleTypeName:
                        string[] coordinates = parametersMatches[0].Groups[1].Value.Split(',');
                        Vector2f p1 = new Vector2f(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                        coordinates = parametersMatches[1].Groups[1].Value.Split(',');
                        Vector2f p2 = new Vector2f(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                        coordinates = parametersMatches[2].Groups[1].Value.Split(',');
                        Vector2f p3 = new Vector2f(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                        shapes.Add(new Triangle(p1, p2, p3));
                        break;
                    case RectangleTypeName:
                        coordinates = parametersMatches[0].Groups[1].Value.Split(',');
                        p1 = new Vector2f(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                        coordinates = parametersMatches[1].Groups[1].Value.Split(',');
                        p2 = new Vector2f(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                        shapes.Add(new Rectangle(p1, p2));
                        break;
                    case CircleTypeName:
                        coordinates = parametersMatches[0].Groups[1].Value.Split(',');
                        p1 = new Vector2f(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                        float r = float.Parse(parametersMatches[1].Groups[1].Value);
                        shapes.Add(new Circle(p1, r));
                        break;
                }
            }

            sr.Dispose();
            return new Canvas.CanvasModel { Name = inputPath, Shapes = shapes, Background = Color.White, Width = 820, Height = 600, SelectedShapes = new List<MyShape>()};
        }

        public static void SetShapeDescription(Canvas.CanvasModel canvas, string outputPath)
        {
            StreamWriter sw = new StreamWriter(outputPath);
            sw.WriteLine(canvas.Name + ":");
            foreach (MyShape shape in canvas.Shapes)
            {
                sw.WriteLine(shape.GetDescription);
            }
            sw.Dispose();
        }
    }
}
