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
//=========
using System.Collections.ObjectModel;
using System.Media;
//=========

namespace Enigma_WPF
{
    /// <summary>
    /// PartSelectingWindow.xaml 的互動邏輯
    /// </summary>
    public partial class RotorSelectingWindow : Window
    {
        public RotorSelectingWindow(EnigmaOperator enigmaOp)
        {
            InitializeComponent();
            //============================
            this.enigmaOp = enigmaOp;
            unusedRots = new ObservableCollection<Rotor>(enigmaOp.UnusedRotors);
            workRots = new ObservableCollection<Rotor>(enigmaOp.WorkingRotors);
        }
        private EnigmaOperator enigmaOp;
        private ObservableCollection<Rotor> unusedRots = new ObservableCollection<Rotor>();
        private ObservableCollection<Rotor> workRots = new ObservableCollection<Rotor>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listBox_AllRotors.ItemsSource = unusedRots;
            listBox_RotorsToWork.ItemsSource = workRots;
        }

        private void button_AddRotor_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_AllRotors.SelectedItem == null) return;
            Rotor selectedRot=(Rotor)listBox_AllRotors.SelectedItem;
            unusedRots.Remove(selectedRot);
            workRots.Add(selectedRot);
        }

        private void button_RemoveRotor_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_RotorsToWork.SelectedItem == null) return;
            Rotor selectedRot = (Rotor)listBox_RotorsToWork.SelectedItem;
            workRots.Remove(selectedRot);
            unusedRots.Add(selectedRot);
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                enigmaOp.ChangeRotors(workRots.ToList(), unusedRots.ToList());
                this.Close();
            }
            catch
            {
                Util.Ding();
                MessageBox.Show("Working rotor number must between 1~5");
            }
        }

        //================================
    }
}
