using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using EnigmaWPF.Model;
using System.ComponentModel;
using Microsoft.Win32;

namespace EnigmaWPF
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

        private EncryptMode encryptMode = EncryptMode.PerChar;

        private PlugBoardWindow pbWindow;

        public MainWindow()
        {
            this.InitializeComponent();
            this.SetWindowsBinding();
            this.SetTooltipBinding();
        }

        private enum EncryptMode
        {
            PerChar,
            PerString
        }

        private void SetWindowsBinding()
        {
            Binding bind;
            bind = new Binding("RotorWindows[0]")
            {
                Source = this.enigmaOp.PartInfo
            };
            this.textBlock_RotorWindow0.SetBinding(TextBlock.TextProperty, bind);
            bind = new Binding("RotorWindows[1]")
            {
                Source = this.enigmaOp.PartInfo
            };
            this.textBlock_RotorWindow1.SetBinding(TextBlock.TextProperty, bind);
            bind = new Binding("RotorWindows[2]")
            {
                Source = this.enigmaOp.PartInfo
            };
            this.textBlock_RotorWindow2.SetBinding(TextBlock.TextProperty, bind);
            bind = new Binding("RotorWindows[3]")
            {
                Source = this.enigmaOp.PartInfo
            };
            this.textBlock_RotorWindow3.SetBinding(TextBlock.TextProperty, bind);
            bind = new Binding("RotorWindows[4]")
            {
                Source = this.enigmaOp.PartInfo
            };
            this.textBlock_RotorWindow4.SetBinding(TextBlock.TextProperty, bind);
        }

        private void SetTooltipBinding()
        {
            Binding bind;
            bind = new Binding("RotorDescriptions[0]")
            {
                Source = this.enigmaOp.PartInfo
            };
            this.textBlock_RotorWindow0.SetBinding(TextBlock.ToolTipProperty, bind);
            bind = new Binding("RotorDescriptions[1]")
            {
                Source = this.enigmaOp.PartInfo
            };
            this.textBlock_RotorWindow1.SetBinding(TextBlock.ToolTipProperty, bind);
            bind = new Binding("RotorDescriptions[2]")
            {
                Source = this.enigmaOp.PartInfo
            };
            this.textBlock_RotorWindow2.SetBinding(TextBlock.ToolTipProperty, bind);
            bind = new Binding("RotorDescriptions[3]")
            {
                Source = this.enigmaOp.PartInfo
            };
            this.textBlock_RotorWindow3.SetBinding(TextBlock.ToolTipProperty, bind);
            bind = new Binding("RotorDescriptions[4]")
            {
                Source = this.enigmaOp.PartInfo
            };
            this.textBlock_RotorWindow4.SetBinding(TextBlock.ToolTipProperty, bind);
            bind = new Binding("ReflectorName")
            {
                Source = this.enigmaOp.PartInfo
            };
            this.textBlock_Reflector.SetBinding(TextBlock.ToolTipProperty, bind);
        }

        private void button_ClearText_Click(object sender, RoutedEventArgs e)
        {
            this.textBox_Input.Clear();
            this.textBox_Output.Clear();
        }

        private void button_Reset_Click(object sender, RoutedEventArgs e)
        {
            this.enigmaOp.ResetRotorPosition();
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
            if (this.encryptMode == EncryptMode.PerChar)
            {
                EncryptChar(e);
            }
        }

        private void EncryptChar(KeyEventArgs e)
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
                char output;
                this.enigmaOp.InputChar(input, out output);
                this.textBox_Input.Text += input;
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

        private void menuItem_ToggleEncryptMode_Click(object sender, RoutedEventArgs e)
        {
            MenuItem modeMenu = (MenuItem)sender;
            if (this.encryptMode == EncryptMode.PerChar)    // 改成一次加密
            {
                this.encryptMode = EncryptMode.PerString;
                modeMenu.Header = "Encrypt Mode: After confirm";
                this.button_Encrypt.IsEnabled = true;
                this.textBox_Input.IsReadOnly = false;
            }
            else    // 改成每次加密
            {
                this.encryptMode = EncryptMode.PerChar;
                modeMenu.Header = "Encrypt Mode: Traditional";
                this.button_Encrypt.IsEnabled = false;
                this.textBox_Input.IsReadOnly = true;
                this.textBox_Input.Clear();
                this.textBox_Output.Clear();
            }
        }

        private void button_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string input = this.textBox_Input.Text;
            string output;
            this.enigmaOp.InputString(input, out output);
            this.textBox_Output.Text = output;
        }

        private void menuItem_Export_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Title = "Export Parts",
                Filter = "dat File (*.dat)|*.dat",
                ValidateNames = true,
                FileName = string.Format("ExpParts{0}.dat", DateTime.Now.ToString("ddMMyyyy"))
            };
            saveDlg.FileOk += this.WriteRotors;
            saveDlg.ShowDialog();
        }

        private void WriteRotors(object saveDialog, CancelEventArgs e)
        {
            SaveFileDialog saveDlg = (SaveFileDialog)saveDialog;
            if (saveDlg.FileName != "")
            {
                PartCollection parts = new PartCollection
                {
                    PlugBoard = this.enigmaOp.PlugBoard,
                    Rotors = this.enigmaOp.WorkingRotors,
                    Reflector = this.enigmaOp.WorkingReflector
                };
                PartWriter writer = new PartWriter(parts);
                try
                {
                    writer.WriteTo(saveDlg.FileName);
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void menuItem_Import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog
            {
                Title="Import Parts",
                Filter = "dat File (*.dat)|*.dat|All File|*.*",
                ValidateNames = true,
            };
            openDlg.FileOk += this.ReadRotors;
            openDlg.ShowDialog();
        }

        private void ReadRotors(object openDialog, CancelEventArgs e)
        {
            OpenFileDialog openDlg = (OpenFileDialog)openDialog;
            PartCollection parts;
            if (openDlg.FileName != "")
            {
                PartReader reader = new PartReader();
                try
                {
                    parts = reader.ReadFromFile(openDlg.FileName);
                    this.enigmaOp.ImportParts(parts);
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "File not found or corrupted", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button_OpenPlugBoardWindow_Click(object sender, RoutedEventArgs e)
        {
            switch (PlugBoardWindow.IsOpened)
            {
                case true:
                    this.pbWindow.Close();
                    this.button_OpenPlugBoardWindow.Content = ">>";
                    break;
                case false:
                    this.pbWindow = new PlugBoardWindow(this.enigmaOp.PlugBoard);
                    this.pbWindow.Show();
                    this.button_OpenPlugBoardWindow.Content = "<<";
                    break;
            }
        }
    }
}
