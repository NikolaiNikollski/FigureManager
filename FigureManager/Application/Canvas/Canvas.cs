using FigureManager.Shapes;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Linq;

namespace FigureManager.Canvas
{
    public class Canvas
    {
        public string Name { get; set; }

        public uint Height { get; set; }

        public uint Width { get; set; }

        public Color Background { get; set; }

        public List<MyShape> Shapes { get; set; }

        public List<MyShape> SelectedShapes { get; set; }

        public void Draw(RenderWindow win)
        {
            win.Clear(Background);
            foreach (MyShape shape in Shapes.Where(s => !SelectedShapes.Contains(s)))
            {
                shape.Draw(win);
            }

            foreach (MyShape selectedShape in SelectedShapes)
            {
                selectedShape.Draw(win);
                selectedShape.GetFrame.Draw(win);
            }
        }

        public bool SelectShape(Vector2f coords, bool withClear)
        {
            if (withClear)
            {
                SelectedShapes = new List<MyShape>();
            }

            MyShape shape = GetShape(coords);
            if (shape != null)
            {
                SelectedShapes.Add(shape);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Fill(Vector2f coords, Color color)
        {
            MyShape shape = GetShape(coords);
            if (shape != null)
            {
                shape.FillColor = color;
            }
            else
            {
                Background = color;
            }
            return true;
        }

        public bool AddShape(Vector2f coords, ShapeType type)
        {
            switch (type)
            {
                case ShapeType.Rectangle:
                    Shapes.Add(new Rectangle(coords));
                    break;
                case ShapeType.Triangle:
                    Shapes.Add(new Triangle(coords));
                    break;
                case ShapeType.Circle:
                    Shapes.Add(new Circle(coords));
                    break;
            }

            return true;
        }

        public bool SetOutlineThickness(int width)
        {
            foreach (MyShape shape in SelectedShapes)
            {
                shape.OutlineThickness = width;
            }

            return SelectedShapes.Count > 0;
        }

        public bool SetOutlineColor(Color color)
        {
            foreach (MyShape shape in SelectedShapes)
            {
                shape.OutlineColor = color;
            }

            return SelectedShapes.Count > 0;
        }

        private MyShape GetShape(Vector2f coords)
        {
            return Shapes.FirstOrDefault(s => s.GetGlobalBounds().Contains(coords.X, coords.Y));
        }
    }
}
