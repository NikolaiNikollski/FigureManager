using FigureManager.Application.Commands;
using FigureManager.Shapes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Linq;

namespace FigureManager
{
    public class Canvas
    {
        public Color Background;
        public List<MyShape> Shapes;
        public List<MyShape> SelectedShapes;
        public MyShape MovingShape;
        public bool IsMove = false;
        public float dX;
        public float dY;

        private readonly Stack<CanvasSnapshot> _commandHistory;

        public Canvas(List<MyShape> shapes, Stack<CanvasSnapshot> commandHistory)
        {
            Shapes = shapes;
            Background = Color.White;
            _commandHistory = commandHistory;
            SelectedShapes = new List<MyShape>();
        }

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

            if (IsMove)
            {
                Vector2i mousePosition = Mouse.GetPosition();
                MovingShape.Position = new Vector2f(mousePosition.X - dX, mousePosition.Y - dY);
                MovingShape.GetFrame.Position = MovingShape.Position;
            }
        }

        public bool SelectShape(Vector2f coords, bool withClear)
        {
            if (withClear)
            {
                SelectedShapes = new List<MyShape>();
            }

            MyShape shape = GetShape(coords);
            if (shape != null && !SelectedShapes.Contains(shape))
            {
                SelectedShapes.Add(shape);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool StartDragAndDrope(Vector2f coords)
        {
            SaveSnapshot();
            MyShape shape = GetShape(coords);
            if (shape == null)
            {
                return false;
            }

            Shapes.Remove(shape);
            Shapes.Insert(0, shape);

            IsMove = true;
            var mousePosition = Mouse.GetPosition();
            dX = mousePosition.X - shape.Position.X;
            dY = mousePosition.Y - shape.Position.Y;

            shape.Position = new Vector2f(shape.Position.X + dX, shape.Position.Y + dY);
            MovingShape = shape;

            return true;
        }

        public bool StopDragAndDrope(Vector2f coords)
        {
            if (IsMove)
            {
                IsMove = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Fill(Vector2f coords, Color color)
        {
            SaveSnapshot();
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

        public bool AddShape(Vector2f coords, ShapeType type, Color color)
        {
            SaveSnapshot();
            switch (type)
            {
                case ShapeType.Rectangle:
                    Shapes.Add(new Rectangle(coords) { FillColor = color });
                    break;
                case ShapeType.Triangle:
                    Shapes.Add(new Triangle(coords) { FillColor = color });
                    break;
                case ShapeType.Circle:
                    Shapes.Add(new Circle(coords) { FillColor = color });
                    break;
            }

            return true;
        }

        public bool SetOutlineThickness(int width)
        {
            SaveSnapshot();
            foreach (MyShape shape in SelectedShapes)
            {
                shape.OutlineThickness = width;
            }

            return SelectedShapes.Count > 0;
        }

        public bool SetOutlineColor(Color color)
        {
            SaveSnapshot();
            foreach (MyShape shape in SelectedShapes)
            {
                shape.OutlineColor = color;
            }

            return SelectedShapes.Count > 0;
        }

        public bool CombineShape()
        {
            SaveSnapshot();
            if (SelectedShapes.Count < 2)
            {
                return false;
            }

            CompoundShape compoundShape = new CompoundShape(SelectedShapes);
            Shapes.Add(compoundShape);
            Shapes = Shapes.Except(SelectedShapes).ToList();
            SelectedShapes = new List<MyShape>();
            SelectedShapes.Add(compoundShape);
            return true;
        }

        public bool DisbandShape()
        {
            SaveSnapshot();
            bool result = false;

            foreach (MyShape shape in SelectedShapes)
            {
                List<CompoundShape> compoundShapes = SelectedShapes.Select(s => s as CompoundShape).Where(s => s != null).ToList();
                foreach (CompoundShape compoundShape in compoundShapes)
                {
                    result = true;
                    Shapes.AddRange(compoundShape.Shapes);
                }
                SelectedShapes = SelectedShapes.Except(compoundShapes.Select(s => (MyShape)s)).ToList();
                Shapes = Shapes.Except(compoundShapes.Select(s => (MyShape)s)).ToList();
            }
            return result;
        }

        private MyShape GetShape(Vector2f coords)
        {
            return Shapes.FirstOrDefault(s => s.GetGlobalBounds().Contains(coords.X, coords.Y));
        }

        private void SaveSnapshot()
        {
            _commandHistory.Push(new CanvasSnapshot(this, Background, Shapes));
        }
    }
}
