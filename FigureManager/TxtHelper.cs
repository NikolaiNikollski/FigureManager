using FigureManager.Decorator;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FigureManager
{
    public static class TxtHelper
    {
        public static List<Shape> LoadShapes()
        {
            List<Shape> shapes = new List<Shape>();

            StreamReader sr = new StreamReader(InputFilePath, Encoding.Default);
            string line;
            while ((line = sr.ReadLine()?.Trim()) != null)
            {
                Match typeMatch = Regex.Match(line, "(.*?):");
                MatchCollection parametersMatches = Regex.Matches(line, "=(.*?)(;|$)");
                string type = typeMatch.Groups[1].Value;

                switch (type)
                {
                    case "TRIANGLE":
                        string[] coordinates = parametersMatches[0].Groups[1].Value.Split(',');
                        Vector2f p1 = new Vector2f(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                        coordinates = parametersMatches[1].Groups[1].Value.Split(',');
                        Vector2f p2 = new Vector2f(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                        coordinates = parametersMatches[2].Groups[1].Value.Split(',');
                        Vector2f p3 = new Vector2f(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                        shapes.Add(new Triangle(p1, p2, p3));
                        break;
                    case "RECTANGLE":
                        coordinates = parametersMatches[0].Groups[1].Value.Split(',');
                        p1 = new Vector2f(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                        coordinates = parametersMatches[1].Groups[1].Value.Split(',');
                        p2 = new Vector2f(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                        shapes.Add(new Rectangle(p1, p2));
                        break;
                    case "CIRCLE":
                        coordinates = parametersMatches[0].Groups[1].Value.Split(',');
                        p1 = new Vector2f(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                        float r = float.Parse(parametersMatches[1].Groups[1].Value);
                        shapes.Add(new Circle(p1, r));
                        break;
                }
            }

            sr.Dispose();
            return shapes;
        }

        public static void SetShapeDescription(List<Shape> shapes)
        {
            StreamWriter sw = new StreamWriter(OutputFilePath);
            foreach (IMathCalculable shape in shapes)
            {
                sw.WriteLine(((IMathCalculable)shape).GetDescription());
            }
            sw.Dispose();
        }
    }
}
