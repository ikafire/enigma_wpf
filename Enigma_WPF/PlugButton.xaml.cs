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
    /// PlugButton.xaml 的互動邏輯
    /// </summary>
    public partial class PlugButton : UserControl
    {
        private static Brush defaultColor = new SolidColorBrush(Colors.SkyBlue);

        private static Brush activatedColor = new SolidColorBrush(Colors.Red);

        private bool isPlugged = false;

        public PlugButton()
        {
            InitializeComponent();
            this.ellipse.Stroke = null;
            this.ellipse.Fill = defaultColor;
        }

        public char Lable
        {
            get
            {
                return this.lable.Text[0];
            }

            set
            {
                this.lable.Text = value.ToString();
            }
        }

        public int PlugNum
        {
            get
            {
                int result;
                if (!int.TryParse(this.plugNum.Text, out result))
                {
                    return -1;
                }

                return result;
            }

            private set
            {
                this.plugNum.Text = value.ToString();
            }
        }

        public bool IsPlugged
        {
            get { return this.isPlugged; }
        }

        public event EventHandler Click;

        public void Activate()
        {
            this.ellipse.Fill = activatedColor;
        }

        public void Deactivate()
        {
            this.ellipse.Fill = defaultColor;
        }

        public void Plug(int plugNum)
        {
            if (this.IsPlugged)
            {
                throw new ArgumentException("It's already plugged");
            }

            this.PlugNum = plugNum;
            this.Deactivate();
            this.isPlugged = true;
        }

        public void Reset()
        {
            this.Deactivate();
            this.plugNum.Text = "";
            this.isPlugged = false;
        }

        private void ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click.Invoke(this, new EventArgs());
            }
        }

        
    }
}
