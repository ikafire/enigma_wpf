using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Enigma_WPF
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Key> availableKeys = new List<Key> 
        { 
            Key.A, Key.B, Key.C, Key.D, Key.E,
            Key.F, Key.G, Key.H, Key.I, Key.J,
            Key.K, Key.L, Key.M, Key.N, Key.O,
            Key.P, Key.Q, Key.R, Key.S, Key.T,
            Key.U, Key.V, Key.W, Key.X, Key.Y,
            Key.Z, Key.Back, Key.Space 
        };
        
        private SoundPlayer typingSound = new SoundPlayer(Properties.Resources.typewriter1);
        
        private SoundPlayer spacingSound = new SoundPlayer(Properties.Resources.typewriter2);
        
        private EnigmaOperator enigmaOp = new EnigmaOperator();

        private bool isMute = false;

        public MainWindow()
        {
            this.InitializeComponent();
            this.SetWindowsBinding();
            this.SetTooltipBinding();
        }

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
            Binding bind0 = new Binding("RotorDescriptions[0]");
            bind0.Source = this.enigmaOp;
            Binding bind1 = new Binding("RotorDescriptions[1]");
            bind1.Source = this.enigmaOp;
            Binding bind2 = new Binding("RotorDescriptions[2]");
            bind2.Source = this.enigmaOp;
            Binding bind3 = new Binding("RotorDescriptions[3]");
            bind3.Source = this.enigmaOp;
            Binding bind4 = new Binding("RotorDescriptions[4]");
            bind4.Source = this.enigmaOp;
            this.textBlock_RotorWindow0.SetBinding(TextBlock.ToolTipProperty, bind0);
            this.textBlock_RotorWindow1.SetBinding(TextBlock.ToolTipProperty, bind1);
            this.textBlock_RotorWindow2.SetBinding(TextBlock.ToolTipProperty, bind2);
            this.textBlock_RotorWindow3.SetBinding(TextBlock.ToolTipProperty, bind3);
            this.textBlock_RotorWindow4.SetBinding(TextBlock.ToolTipProperty, bind4);
            Binding refBind = new Binding("ReflectorName");
            refBind.Source = this.enigmaOp;
            this.textBlock_Reflector.SetBinding(TextBlock.ToolTipProperty, refBind);
        }

        private void button_ClearText_Click(object sender, RoutedEventArgs e)
        {
            this.textBox_Input.Clear();
            this.textBox_Output.Clear();
        }

        private void button_Reset_Click(object sender, RoutedEventArgs e)
        {
            this.enigmaOp.ResetRotorPosition();
            this.textBox_Input.Clear();
            this.textBox_Output.Clear();
        }

        private void button_RotorTurn_Click(object sender, RoutedEventArgs e)
        {
            Button turnButton = (Button)sender;
            string tag = turnButton.Tag.ToString();
            TurningDirection direction;
            int rotNum;
            if (tag[0] == 'f')
            {
                direction = TurningDirection.Forward;
            }
            else if (tag[0] == 'r')
            {
                direction = TurningDirection.Reverse;
            }
            else
            {
                throw new ArgumentException("Invalid button tag ({0})", turnButton.Name);
            }

            rotNum = int.Parse(tag[1].ToString());
            this.enigmaOp.TurnRotor(rotNum, direction);
        }

        private void grid_Main_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!this.availableKeys.Contains(e.Key))
            {
                return;
            }

            if (e.Key == Key.Back)
            {
                int length = textBox_Input.Text.Length;
                if (length <= 0)
                {
                    return;
                }

                this.textBox_Input.Text = textBox_Input.Text.Remove(length - 1);
                this.textBox_Output.Text = textBox_Output.Text.Remove(length - 1);
            }
            else if (e.Key == Key.Space)
            {
                if (!this.isMute)
                {
                    this.spacingSound.Play();
                }

                this.textBox_Input.Text += '_';
                this.textBox_Output.Text += '_';
            }
            else
            {
                if (!this.isMute)
                {
                    this.typingSound.Play();
                }

                char input = e.Key.ToString()[0];
                this.textBox_Input.Text += input;
                char output;
                this.enigmaOp.InputChar(input, out output);
                this.textBox_Output.Text += output;
            }
        }

        private void menuItem_ToggleSound_Click(object sender, RoutedEventArgs e)
        {
            MenuItem soundMenu = (MenuItem)sender;
            if (this.isMute)
            {
                this.isMute = false;
                soundMenu.Header = "Sound: On";
            }
            else
            {
                this.isMute = true;
                soundMenu.Header = "Sound: Off";
            }
        }

        private void menuItem_SelectRotor_Click(object sender, RoutedEventArgs e)
        {
            RotorSelectingWindow selectWindow = new RotorSelectingWindow(this.enigmaOp);
            selectWindow.ShowDialog();
        }
    }
}
