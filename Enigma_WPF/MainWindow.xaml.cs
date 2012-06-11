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
//========================
using System.Media;
//========================

namespace Enigma_WPF
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //==================
            SetWindowsBinding();
            //==================
        }
        //==========================================
        List<Key> availableKeys = new List<Key> { Key.A, Key.B, Key.C, Key.D, Key.E, Key.F, Key.G, Key.H, Key.I, Key.J, Key.K, Key.L, Key.M, Key.N, Key.O, Key.P, Key.Q, Key.R, Key.S, Key.T, Key.U, Key.V, Key.W, Key.X, Key.Y, Key.Z, Key.Back, Key.Space };
        SoundPlayer typingSound = new SoundPlayer(Properties.Resources.typewriter1);
        SoundPlayer spacingSound = new SoundPlayer(Properties.Resources.typewriter2);
        Operator op = new Operator();
        bool mute = false;
        private void SetWindowsBinding()
        {
            Binding windowBind0 = new Binding("RotorWindow0");
            windowBind0.Source = this.op;
            Binding windowBind1 = new Binding("RotorWindow1");
            windowBind1.Source = this.op;
            Binding windowBind2 = new Binding("RotorWindow2");
            windowBind2.Source = this.op;
            textBlock_RotorWindow0.SetBinding(TextBlock.TextProperty, windowBind0);
            textBlock_RotorWindow1.SetBinding(TextBlock.TextProperty, windowBind1);
            textBlock_RotorWindow2.SetBinding(TextBlock.TextProperty, windowBind2);
        }

        private void button_ClearText_Click(object sender, RoutedEventArgs e)
        {
            textBox_Input.Text = string.Empty;
            textBox_Output.Text = string.Empty;
        }

        private void button_Mute_Click(object sender, RoutedEventArgs e)
        {
            Button muteButton = (Button)sender;
            if (muteButton.Tag.ToString() == "on")  //which means SE is now on
            {
                mute = true;
                muteButton.Tag = "off";
                muteButton.Content = "Sound : Off";
            }
            else
            {
                mute = false;
                muteButton.Tag = "on";
                muteButton.Content = "Sound : On";
            }
        }

        private void button_Reset_Click(object sender, RoutedEventArgs e)
        {
            op.ResetRotorPosition();
            textBox_Input.Text = string.Empty;
            textBox_Output.Text = string.Empty;
        }

        private void button_RotorTurn_Click(object sender, RoutedEventArgs e)
        {
            Button turnButton = (Button)sender;
            string tag = turnButton.Tag.ToString();
            TurningDirection direction;
            int rotNum;
            if (tag[0] == 'f')
                direction = TurningDirection.Forward;
            else if (tag[0] == 'r')
                direction = TurningDirection.Reverse;
            else
                throw new ArgumentException("Invalid button tag ({0})", turnButton.Name);
            rotNum = int.Parse(tag[1].ToString());
            op.TurnRotor(rotNum, direction);
        }

        private void grid_Main_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!availableKeys.Contains(e.Key)) return;
            if (e.Key == Key.Back)
            {
                int length = textBox_Input.Text.Length;
                if (length <= 0) return;
                textBox_Input.Text = textBox_Input.Text.Remove(length - 1);
                textBox_Output.Text = textBox_Output.Text.Remove(length - 1);
            }
            else if (e.Key == Key.Space)
            {
                if (!mute) spacingSound.Play();
                textBox_Input.Text += '_';
                textBox_Output.Text += '_';
            }
            else
            {
                if (!mute) typingSound.Play();
                char input = e.Key.ToString()[0];
                textBox_Input.Text += input;
                char output;
                op.InputChar(input, out output);
                textBox_Output.Text += output;
            }

        }
        //==========================================
    }
}
