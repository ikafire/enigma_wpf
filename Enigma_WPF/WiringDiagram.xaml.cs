using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EnigmaWPF
{
    /// <summary>
    /// WiringDiagram.xaml 的互動邏輯
    /// </summary>
    public partial class WiringDiagram : UserControl
    {
        private Brush defaultColor = new SolidColorBrush(Colors.SkyBlue);

        private Brush activatedColor = new SolidColorBrush(Colors.Red);

        private Ellipse activatedEllipse = null;

        private Ellipse[] rightEllipses;

        private Ellipse[] leftEllipses;

        private Line[] lines;

        public WiringDiagram()
        {
            this.InitializeComponent();
            this.AddEllipseToList();
            this.CreateLines();
            this.Wiring = new int[26];
            for (int i = 0; i < this.Wiring.Length; i++)
            {
                this.Wiring[i] = -1;
            }
        }

        public int[] Wiring { get; set; }

        public bool IsReadOnly { get; set; }

        public void Refresh()
        {
            for (int rIndex = 0; rIndex < 26; rIndex++)
            {
                int lIndex = this.Wiring[rIndex];
                this.Link(rIndex, lIndex);
            }
        }

        private void AddEllipseToList()
        {
            rightEllipses = new Ellipse[26]
            {
                ellipse_Right0,
                ellipse_Right1,
                ellipse_Right2,
                ellipse_Right3,
                ellipse_Right4,
                ellipse_Right5,
                ellipse_Right6,
                ellipse_Right7,
                ellipse_Right8,
                ellipse_Right9,
                ellipse_Right10,
                ellipse_Right11,
                ellipse_Right12,
                ellipse_Right13,
                ellipse_Right14,
                ellipse_Right15,
                ellipse_Right16,
                ellipse_Right17,
                ellipse_Right18,
                ellipse_Right19,
                ellipse_Right20,
                ellipse_Right21,
                ellipse_Right22,
                ellipse_Right23,
                ellipse_Right24,
                ellipse_Right25
            };
            leftEllipses = new Ellipse[26]
            {
                ellipse_Left0,
                ellipse_Left1,
                ellipse_Left2,
                ellipse_Left3,
                ellipse_Left4,
                ellipse_Left5,
                ellipse_Left6,
                ellipse_Left7,
                ellipse_Left8,
                ellipse_Left9,
                ellipse_Left10,
                ellipse_Left11,
                ellipse_Left12,
                ellipse_Left13,
                ellipse_Left14,
                ellipse_Left15,
                ellipse_Left16,
                ellipse_Left17,
                ellipse_Left18,
                ellipse_Left19,
                ellipse_Left20,
                ellipse_Left21,
                ellipse_Left22,
                ellipse_Left23,
                ellipse_Left24,
                ellipse_Left25
            };
        }

        private void CreateLines()
        {
            List<Line> tmp = new List<Line>(26);
            foreach (var ellipse in this.rightEllipses)
            {
                Point center = ellipse.GetCanvasCenter();
                Line line = new Line
                {
                    X1=center.X,
                    X2=center.X,
                    Y1=center.Y,
                    Y2=center.Y,
                    Stroke = defaultColor,
                    StrokeThickness=2
                };
                tmp.Add(line);
                this.canvas_Main.Children.Add(line);
            }
            this.lines = tmp.ToArray();
        }

        private void Link(int rIndex, int lIndex)
        {
            Point center = this.leftEllipses[lIndex].GetCanvasCenter();
            Line line = this.lines[rIndex];
            line.X2 = center.X;
            line.Y2 = center.Y;
            this.Wiring[rIndex] = lIndex;
        }

        private void UnLink(int rIndex, int lIndex)
        {
            Line line = this.lines[rIndex];
            line.X2 = line.X1;
            line.Y2 = line.Y1;
            this.Wiring[rIndex] = -1;
        }

        private void MarkWithColor(Ellipse rightEllipse, Brush color)
        {
            int rIndex, lIndex;
            this.FindIndex(rightEllipse, out rIndex, out lIndex);
            if (this.activatedEllipse != rightEllipse)
            {
                this.rightEllipses[rIndex].Fill = color;
            }
            this.lines[rIndex].Stroke = color;
            if (lIndex >= 0)
            {
                this.leftEllipses[lIndex].Fill = color;
            }
        }

        private void FindIndex(Ellipse rightEllipse, out int rIndex, out int lIndex)
        {
            rIndex = Array.FindIndex(this.rightEllipses, ell => ell == rightEllipse);
            lIndex = this.Wiring[rIndex];
        }

        private void ellipse_Left_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsReadOnly)
            {
                return;
            }

            if (this.activatedEllipse == null)
            {
                return;
            }

            int rIndex, lIndex;
            this.FindIndex(activatedEllipse, out rIndex, out lIndex);


            if (lIndex < 0)
            {
                lIndex = Array.FindIndex(this.leftEllipses, ell => ell == (Ellipse)sender);
                if (this.Wiring.Contains(lIndex))
                {
                    return;
                }

                this.Link(rIndex, lIndex);
                this.activatedEllipse.Fill = this.defaultColor;
                this.activatedEllipse = null;
            }
            else if (this.leftEllipses[lIndex] == (Ellipse)sender)
            {
                this.UnLink(rIndex,lIndex);
                this.activatedEllipse.Fill = this.defaultColor;
                this.activatedEllipse = null;
            }
            else if (lIndex >= 0)
            {
                return;
            }
        }

        private void ellipse_Right_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsReadOnly)
            {
                return;
            }

            Ellipse rEllipse = (Ellipse)sender;

            if (this.activatedEllipse == null)
            {
                this.activatedEllipse = rEllipse;
                this.activatedEllipse.Fill = this.activatedColor;
            }
            else if (this.activatedEllipse == rEllipse)
            {
                this.activatedEllipse.Fill = this.defaultColor;
                this.activatedEllipse = null;
            }
            else
            {
                this.activatedEllipse.Fill = this.defaultColor;
                this.activatedEllipse = rEllipse;
                this.activatedEllipse.Fill = this.activatedColor;
            }
        }

        private void ellipse_Right_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MarkWithColor((Ellipse)sender, this.activatedColor);
        }

        private void ellipse_Right_MouseLeave(object sender, MouseEventArgs e)
        {
            this.MarkWithColor((Ellipse)sender, this.defaultColor);
        }

        private void ellipse_Left_MouseLeave(object sender, MouseEventArgs e)
        {
            Ellipse lEllipse = (Ellipse)sender;
            int lIndex = Array.FindIndex(this.leftEllipses, ell => ell == lEllipse);
            int rIndex = Array.FindIndex(this.Wiring, i => i == lIndex);
            if (rIndex<0)
            {
                return;
            }

            this.MarkWithColor(this.rightEllipses[rIndex], this.defaultColor);
        }

        private void ellipse_Left_MouseEnter(object sender, MouseEventArgs e)
        {
            Ellipse lEllipse = (Ellipse)sender;
            int lIndex = Array.FindIndex(this.leftEllipses, ell => ell == lEllipse);
            int rIndex = Array.FindIndex(this.Wiring, i => i == lIndex);
            if (rIndex < 0)
            {
                return;
            }

            this.MarkWithColor(this.rightEllipses[rIndex], this.activatedColor);
        }

    }
}
