using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Enigma_WPF
{
    /// <summary>
    /// PartSelectingWindow.xaml 的互動邏輯
    /// </summary>
    public partial class RotorSelectingWindow : Window
    {
        private EnigmaOperator enigmaOp;

        private ObservableCollection<Rotor> unusedRots = new ObservableCollection<Rotor>();

        private ObservableCollection<Rotor> workRots = new ObservableCollection<Rotor>();

        public RotorSelectingWindow(EnigmaOperator enigmaOp)
        {
            this.InitializeComponent();
            this.enigmaOp = enigmaOp;
            this.unusedRots = new ObservableCollection<Rotor>(this.enigmaOp.UnusedRotors);
            this.workRots = new ObservableCollection<Rotor>(this.enigmaOp.WorkingRotors);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.listBox_AllRotors.ItemsSource = this.unusedRots;
            this.listBox_RotorsToWork.ItemsSource = this.workRots;
        }

        /// <summary>
        /// Move rotor from unused list to work list
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void button_AddRotor_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_AllRotors.SelectedItem == null)
            {
                return;
            }

            Rotor selectedRot = (Rotor)this.listBox_AllRotors.SelectedItem;
            this.unusedRots.Remove(selectedRot);
            this.workRots.Add(selectedRot);
        }

        /// <summary>
        /// Move rotor from work list to unused list
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void button_RemoveRotor_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_RotorsToWork.SelectedItem == null)
            {
                return;
            }

            Rotor selectedRot = (Rotor)this.listBox_RotorsToWork.SelectedItem;
            this.workRots.Remove(selectedRot);
            this.unusedRots.Add(selectedRot);
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.enigmaOp.ChangeRotors(this.workRots.ToList(), this.unusedRots.ToList());
                this.Close();
            }
            catch (ArgumentOutOfRangeException error)
            {
                Util.Ding();
                MessageBox.Show(error.Message);
            }
        }

        private void button_Sort_Click(object sender, RoutedEventArgs e)
        {
            this.unusedRots = this.unusedRots.Sorted();
            this.listBox_AllRotors.ItemsSource = this.unusedRots;
        }
    }
}
