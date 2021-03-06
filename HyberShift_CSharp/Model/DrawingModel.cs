﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HyberShift_CSharp.Model
{
    public class DrawingModel
    {
        //getter and setter
        //public Color Color {get;set;}
        //public int Thickness { get; set; }

        public void Draw(Canvas canvas, ObservableCollection<Point> points, int thickness, Color color)
        {
            for (var i = 0; i < points.Count - 1; i++)
            {
                var line = new Line();
                line.Stroke = new SolidColorBrush(color);
                line.StrokeThickness = thickness;
                line.X1 = points[i].X;
                line.Y1 = points[i].Y;
                line.X2 = points[i + 1].X;
                line.Y2 = points[i + 1].Y;
                canvas.Children.Add(line);
            }
        }
    }
}