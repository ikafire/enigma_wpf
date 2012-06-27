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

        private ObservableCollection<Rotor> allRots = new ObservableCollection<Rotor>();

        private ObservableCollection<Rotor> workRots = new ObservableCollection<Rotor>();

        public RotorSelectingWindow(EnigmaOperator enigmaOp)
        {
            this.InitializeComponent();
            this.enigmaOp = enigmaOp;
            this.allRots = new ObservableCollection<Rotor>(this.enigmaOp.AllRotors);
            this.workRots = new ObservableCollection<Rotor>(this.enigmaOp.WorkingRotors);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.listBox_AllRotors.ItemsSource = this.allRots;
            this.listBox_WorkRotors.ItemsSource = this.workRots;
        }

        private void button_AddRotor_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_AllRotors.SelectedItem == null)
            {
                return;
            }

            if (this.workRots.Count >= 5)
            {
                return;
            }

            Rotor selectedRot = (Rotor)this.listBox_AllRotors.SelectedItem;
            if (this.workRots.Contains(selectedRot))
            {
                return;
            }

            this.workRots.Add(selectedRot);
        }

        private void button_RemoveRotor_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_WorkRotors.SelectedItem == null)
            {
                return;
            }

            Rotor selectedRot = (Rotor)this.listBox_WorkRotors.SelectedItem;
            this.workRots.Remove(selectedRot);
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
            this.Close();
        }
    }
}
