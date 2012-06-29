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
using System.Diagnostics;

namespace EnigmaWPF
{
    /// <summary>
    /// PlugBoardWindow.xaml 的互動邏輯
    /// </summary>
    public partial class PlugBoardWindow : Window
    {
        private PlugBoard plugboard;

        private PlugButton activatedPlug = null;

        private PlugButton[] plugButtons;

        public PlugBoardWindow(PlugBoard plugboard)
        {
            InitializeComponent();
            this.plugButtons = new PlugButton[26] 
            { 
                plugButton_A, 
                plugButton_B, 
                plugButton_C, 
                plugButton_D,
                plugButton_E, 
                plugButton_F, 
                plugButton_G, 
                plugButton_H,
                plugButton_I, 
                plugButton_J, 
                plugButton_K, 
                plugButton_L,
                plugButton_M, 
                plugButton_N, 
                plugButton_O, 
                plugButton_P,
                plugButton_Q, 
                plugButton_R, 
                plugButton_S, 
                plugButton_T,
                plugButton_U, 
                plugButton_V, 
                plugButton_W, 
                plugButton_X,
                plugButton_Y, 
                plugButton_Z
            };
            this.plugboard = plugboard;
            this.ShowPlugs();
        }

        public static bool IsOpened { get; set; }

        private void ShowPlugs()
        {
            foreach (var button in plugButtons)
            {
                button.Reset();
            }
            for (int i = 0; i < this.plugboard.Plugs.Count; i++)
            {
                this.plugButtons[this.plugboard.Plugs[i].X].Plug(i);
                this.plugButtons[this.plugboard.Plugs[i].Y].Plug(i);
            }
        }

        private int IndexOfPlug(PlugButton pButton)
        {
            return Array.FindIndex<PlugButton>(this.plugButtons, btn => btn == pButton);
        }

        private void PlugButton_Click(object sender, EventArgs e)
        {
            PlugButton pButton = (PlugButton)sender;
            if (this.activatedPlug == null)
            {
                if (!pButton.IsPlugged)
                {
                    pButton.Activate();
                    this.activatedPlug = pButton;
                }
                else
                {
                    int index1=this.IndexOfPlug(pButton);
                    int index2;
                    this.plugboard.MutateSignal(index1, out index2);
                    Debug.Assert(index1 != index2);
                    this.plugboard.UnPlug(index1, index2);
                    this.ShowPlugs();
                }
            }
            else if (this.activatedPlug==pButton)
            {
                pButton.Deactivate();
                this.activatedPlug = null;
            }
            else if (!pButton.IsPlugged)
            {
                int index1=this.IndexOfPlug(pButton);
                int index2=this.IndexOfPlug(this.activatedPlug);
                this.plugboard.Plug(index1, index2);
                this.activatedPlug = null;
                this.ShowPlugs();
            }
            else if (pButton.IsPlugged)
            {
                return;
            }
        }

        private void button_Reset_Click(object sender, RoutedEventArgs e)
        {
            this.plugboard.UnPlugAll();
            this.ShowPlugs();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsOpened = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            IsOpened = false;
        }
    }
}
