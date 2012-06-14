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
using System.Collections.ObjectModel;
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
            SetTooltipBinding();
            //==================
        }
        //==========================================
        List<Key> availableKeys = new List<Key> { Key.A, Key.B, Key.C, Key.D, Key.E, Key.F, Key.G, Key.H, Key.I, Key.J, Key.K, Key.L, Key.M, Key.N, Key.O, Key.P, Key.Q, Key.R, Key.S, Key.T, Key.U, Key.V, Key.W, Key.X, Key.Y, Key.Z, Key.Back, Key.Space };
        SoundPlayer typingSound = new SoundPlayer(Properties.Resources.typewriter1);
        SoundPlayer spacingSound = new SoundPlayer(Properties.Resources.typewriter2);
        EnigmaOperator enigmaOp = new EnigmaOperator();
        bool isMute = false;
        private void SetWindowsBinding()
        {
            Binding windowBind0 = new Binding("RotorWindows[0]");
            windowBind0.Source = this.enigmaOp;
            Binding windowBind1 = new Binding("RotorWindows[1]");
            windowBind1.Source = this.enigmaOp;
            Binding windowBind2 = new Binding("RotorWindows[2]");
            windowBind2.Source = this.enigmaOp;
            Binding windowBind3 = new Binding("RotorWindows[3]");
            windowBind3.Source = this.enigmaOp;
            Binding windowBind4 = new Binding("RotorWindows[4]");
            windowBind4.Source = this.enigmaOp;
            textBlock_RotorWindow0.SetBinding(TextBlock.TextProperty, windowBind0);
            textBlock_RotorWindow1.SetBinding(TextBlock.TextProperty, windowBind1);
            textBlock_RotorWindow2.SetBinding(TextBlock.TextProperty, windowBind2);
            textBlock_RotorWindow3.SetBinding(TextBlock.TextProperty, windowBind3);
            textBlock_RotorWindow4.SetBinding(TextBlock.TextProperty, windowBind4);
        }
        private void SetTooltipBinding()
        {
            Binding bind0 = new Binding("RotorNames[0]");
            bind0.Source = this.enigmaOp;
            Binding bind1 = new Binding("RotorNames[1]");
            bind1.Source = this.enigmaOp;
            Binding bind2 = new Binding("RotorNames[2]");
            bind2.Source = this.enigmaOp;
            Binding bind3 = new Binding("RotorNames[3]");
            bind3.Source = this.enigmaOp;
            Binding bind4 = new Binding("RotorNames[4]");
            bind4.Source = this.enigmaOp;
            textBlock_RotorWindow0.SetBinding(TextBlock.ToolTipProperty, bind0);
            textBlock_RotorWindow1.SetBinding(TextBlock.ToolTipProperty, bind1);
            textBlock_RotorWindow2.SetBinding(TextBlock.ToolTipProperty, bind2);
            textBlock_RotorWindow3.SetBinding(TextBlock.ToolTipProperty, bind3);
            textBlock_RotorWindow4.SetBinding(TextBlock.ToolTipProperty, bind4);
            Binding refBind = new Binding("ReflectorName");
            refBind.Source = this.enigmaOp;
            textBlock_Reflector.SetBinding(TextBlock.ToolTipProperty, refBind);
        }

        private void button_ClearText_Click(object sender, RoutedEventArgs e)
        {
            textBox_Input.Clear();
            textBox_Output.Clear();
        }

        private void button_Reset_Click(object sender, RoutedEventArgs e)
        {
            enigmaOp.ResetRotorPosition();
            textBox_Input.Clear();
            textBox_Output.Clear();
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
            enigmaOp.TurnRotor(rotNum, direction);
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
                if (!isMute) spacingSound.Play();
                textBox_Input.Text += '_';
                textBox_Output.Text += '_';
            }
            else
            {
                if (!isMute) typingSound.Play();
                char input = e.Key.ToString()[0];
                textBox_Input.Text += input;
                char output;
                enigmaOp.InputChar(input, out output);
                textBox_Output.Text += output;
            }
        }

        private void menuItem_ToggleSound_Click(object sender, RoutedEventArgs e)
        {
            MenuItem soundMenu = (MenuItem)sender;
            if (isMute)
            {
                isMute = false;
                soundMenu.Header = "Sound: On";
            }
            else
            {
                isMute = true;
                soundMenu.Header = "Sound: Off";
            }
        }

        private void menuItem_SelectRotor_Click(object sender, RoutedEventArgs e)
        {
            RotorSelectingWindow selectWindow = new RotorSelectingWindow(this.enigmaOp);
            selectWindow.ShowDialog();
        }

        //private void button_Paste_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!Clipboard.ContainsText()) return;
        //    textBox_Input.Clear();
        //    textBox_Output.Clear();
        //    string input = Clipboard.GetText().ToUpperInvariant();
        //    string output = string.Empty;
        //    char outChar;
        //    foreach (char c in input)
        //    {
        //        try
        //        {
        //            enigmaOp.InputChar(c, out outChar);
        //            output += outChar;
        //        }
        //        catch
        //        {
        //            output += c;
        //        }
        //    }
        //    textBox_Input.Text = input;
        //    textBox_Output.Text = output;
        //}

        //private void button_Copy_Click(object sender, RoutedEventArgs e)
        //{
        //    Clipboard.SetText(textBox_Output.Text);
        //}

        //==========================================
    }
}
