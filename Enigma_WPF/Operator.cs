using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Enigma_WPF
{
    class Operator : INotifyPropertyChanged
    {
        private EnigmaMachine enigma;
        private static readonly char[] convert = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private char[] rotorWindows = new char[5];    //WARNING: DO NOT SET THIS VALUE DIRECTLY, SET IT THROUGH PROPERTIES
        public event PropertyChangedEventHandler PropertyChanged;

        public char RotorWindow0
        {
            get { return rotorWindows[0]; }
            private set { SetWindow(0, value); }
        }
        public char RotorWindow1
        {
            get { return rotorWindows[1]; }
            private set { SetWindow(1, value); }
        }
        public char RotorWindow2
        {
            get { return rotorWindows[2]; }
            private set { SetWindow(2, value); }
        }
        public Operator()
        {
            enigma = new EnigmaMachine();
            UpdateWindows();
        }
        public void InputChar(char input, out char output)
        {
            int signal = CharToInt(input);
            enigma.InputSignal(signal, out signal);
            output = IntToChar(signal);
            UpdateWindows();
        }
        public void TurnRotor(int rotorNum, TurningDirection direction)
        {
            Rotor rot = enigma.GetRotor(rotorNum);
            switch (direction)
            {
                case TurningDirection.Forward:
                    rot.ForwardTurn();
                    break;
                case TurningDirection.Reverse:
                    rot.ReverseTurn();
                    break;
            }
            UpdateWindows();
        }
        public void ResetRotorPosition()
        {
            Rotor[] rots = enigma.GetAllRotors();
            foreach (Rotor rot in rots)
            { 
                rot.ResetPosition();
            }
            UpdateWindows();
        }

        private void UpdateWindows()
        {
            Rotor[] rotors = enigma.GetAllRotors();
            RotorWindow0 = IntToChar(rotors[0].CurrentPosition);
            RotorWindow1 = IntToChar(rotors[1].CurrentPosition);
            RotorWindow2 = IntToChar(rotors[2].CurrentPosition);
        }
        private int CharToInt(char c)
        {
            return Array.FindIndex(convert, target => target == c);
        }
        private char IntToChar(int num)
        {
            return convert[num];
        }
        private bool SetWindow(int windowNum, char value)
        {
            if (rotorWindows[windowNum] == value) return false;
            rotorWindows[windowNum] = value;
            if (PropertyChanged != null)
            {
                string propertyName = String.Format("RotorWindow{0}", windowNum);
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            return true;
        }
    }
}