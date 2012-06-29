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
using System.Windows.Shapes;
using EnigmaWPF.Model;

namespace EnigmaWPF
{
    /// <summary>
    /// PartDialogWindow.xaml 的互動邏輯
    /// </summary>
    public partial class PartDialogWindow : Window
    {
        private bool isReadOnly = true;

        public PartDialogWindow(IPart part)
        {
            this.InitializeComponent();
            this.wiringDiagram.Wiring = part.Wiring;
            this.wiringDiagram.Refresh();
            this.textBlock_Name.Text = part.Name;
            if (part.Notches == null)
            {
                this.textBlock_Notch.Text = "--";
            }
            else
            {
                this.textBlock_Notch.Text = "";
                foreach (int notch in part.Notches)
                {
                    this.textBlock_Notch.Text += Util.IntToChar(notch);
                }
            }
        }

        public bool IsReadOnly 
        {
            get
            {
                return this.isReadOnly;
            }

            set
            {
                this.isReadOnly = value;
                this.wiringDiagram.IsReadOnly = value;
            }
        }
    }
}
