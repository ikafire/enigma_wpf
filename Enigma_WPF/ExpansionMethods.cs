using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace EnigmaWPF
{
    public static class ExpansionMethods
    {
        public static Point GetCanvasCenter(this Ellipse ellipse)
        {
            double x, y;
            x = Canvas.GetLeft(ellipse) + ellipse.Width / 2d;
            y = Canvas.GetTop(ellipse) + ellipse.Height / 2d;
            return new Point(x, y);
        }
    }
}
