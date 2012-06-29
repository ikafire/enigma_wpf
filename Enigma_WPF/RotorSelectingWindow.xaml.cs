using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using EnigmaWPF.Model;

namespace EnigmaWPF
{
    /// <summary>
    /// PartSelectingWindow.xaml 的互動邏輯
    /// </summary>
    public partial class RotorSelectingWindow : Window
    {
        private EnigmaOperator enigmaOp;

        private ObservableCollection<Rotor> allRots;

        private ObservableCollection<Rotor> workRots;

        private ObservableCollection<Reflector> allRefs;

        public RotorSelectingWindow(EnigmaOperator enigmaOp)
        {
            this.InitializeComponent();
            this.enigmaOp = enigmaOp;
            this.allRots = new ObservableCollection<Rotor>(this.enigmaOp.AllRotors);
            this.workRots = new ObservableCollection<Rotor>(this.enigmaOp.WorkingRotors);
            this.allRefs = new ObservableCollection<Reflector>(this.enigmaOp.AllReflectors);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.listBox_AllRotors.ItemsSource = this.allRots;
            this.listBox_WorkRotors.ItemsSource = this.workRots;
            this.listBox_AllReflectors.ItemsSource = this.allRefs;
            this.listBox_AllReflectors.SelectedItem = this.enigmaOp.WorkingReflector;
        }

        private void AddToWorkRotor()
        {
            if (listBox_AllRotors.SelectedItem == null)
            {
                Util.Ding();
                return;
            }

            if (this.workRots.Count >= 5)
            {
                Util.Ding();
                return;
            }

            Rotor selectedRot = (Rotor)this.listBox_AllRotors.SelectedItem;
            if (this.workRots.Contains(selectedRot))
            {
                Util.Ding();
                return;
            }

            this.workRots.Add(selectedRot);
        }

        private void RemoveFromWorkRotor()
        {
            if (listBox_WorkRotors.SelectedItem == null)
            {
                Util.Ding();
                return;
            }

            Rotor selectedRot = (Rotor)this.listBox_WorkRotors.SelectedItem;
            this.workRots.Remove(selectedRot);
        }

        private void button_AddRotor_Click(object sender, RoutedEventArgs e)
        {
            this.AddToWorkRotor();
        }

        private void button_RemoveRotor_Click(object sender, RoutedEventArgs e)
        {
            this.RemoveFromWorkRotor();
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (this.workRots.Count <= 0)
            {
                Util.Ding();
                MessageBox.Show("There must be at least one rotor to work.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            this.enigmaOp.ChangeRotors(this.workRots.ToList());
            this.enigmaOp.ChangeReflector((Reflector)this.listBox_AllReflectors.SelectedItem);
            this.Close();
        }

        private void listBox_AllRotors_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.AddToWorkRotor();
        }

        private void listBox_WorkRotors_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.RemoveFromWorkRotor();
        }

        private void button_RotorMoveUP_Click(object sender, RoutedEventArgs e)
        {
            int index = this.listBox_WorkRotors.SelectedIndex;
            if (index > 0)
            {
                this.workRots.Move(index, index - 1);
            }
        }

        private void button_RotorMoveDown_Click(object sender, RoutedEventArgs e)
        {
            int index = this.listBox_WorkRotors.SelectedIndex;
            if (index < this.workRots.Count - 1)
            {
                this.workRots.Move(index, index + 1);
            }
        }

        private void button_ShowRotorWiring_Click(object sender, RoutedEventArgs e)
        {
            IPart source = (IPart)this.listBox_AllRotors.SelectedItem;
            PartDialogWindow pdWindow = new PartDialogWindow(source);
            pdWindow.IsReadOnly = true;
            pdWindow.ShowDialog();
        }

        private void button_ShowReflectorWiring_Click(object sender, RoutedEventArgs e)
        {
            IPart source = (IPart)this.listBox_AllReflectors.SelectedItem;
            PartDialogWindow pdWindow = new PartDialogWindow(source);
            pdWindow.IsReadOnly = true;
            pdWindow.ShowDialog();
        }
    }
}
