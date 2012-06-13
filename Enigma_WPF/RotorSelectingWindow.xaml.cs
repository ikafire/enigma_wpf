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
            this.enigmaOp = enigmaOp;
            allRotors = new ObservableCollection<Rotor>(enigmaOp.AllRotors);
            workRotors = new ObservableCollection<Rotor>(enigmaOp.WorkingRotors);
            workRotors.RemoveNull();
            workRotors.ForEach(workRot => allRotors.Remove(workRot));
        }

        //================================
        private EnigmaOperator enigmaOp;
        private ObservableCollection<Rotor> allRotors = new ObservableCollection<Rotor>();
        private ObservableCollection<Rotor> workRotors = new ObservableCollection<Rotor>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listBox_AllRotors.ItemsSource = allRotors;
            listBox_RotorsToWork.ItemsSource = workRotors;
        }

        private void button_AddRotor_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_AllRotors.SelectedItem == null) return;
            Rotor selectedRot=(Rotor)listBox_AllRotors.SelectedItem;
            allRotors.Remove(selectedRot);
            workRotors.Add(selectedRot);
        }

        private void button_RemoveRotor_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_RotorsToWork.SelectedItem == null) return;
            Rotor selectedRot = (Rotor)listBox_RotorsToWork.SelectedItem;
            workRotors.Remove(selectedRot);
            allRotors.Add(selectedRot);
        }

        //================================
    }
}
