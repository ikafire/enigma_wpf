using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Enigma_WPF
{
    class EnigmaOperator : INotifyPropertyChanged
    {
        private EnigmaMachine enigma;
        private static readonly char[] convert = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private char[] rotorWindows = new char[5];    //WARNING: DO NOT SET THIS VALUE DIRECTLY, USE UpdateWindows()
        private string[] rotorNames = new string[5];  //WARINIG: DO NOT SET THIS VALUE DIRECTLY, USE UpdatePartNames()
        private string reflectorName;   //WARINIG: DO NOT SET THIS VALUE DIRECTLY, USE UpdatePartNames()
        public event PropertyChangedEventHandler PropertyChanged;
        public char[] RotorWindows
        {
            get { return rotorWindows; }
        }
        public string[] RotorNames
        {
            get { return rotorNames; }
        }
        public string ReflectorName
        {
            get { return reflectorName; }
        }
        public EnigmaOperator()
        {
            enigma = new EnigmaMachine();
            UpdateWindows();
            UpdatePartNames();
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
            if (rot == null) return;
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
                if (rot != null)
                {
                    rot.ResetPosition();
                }
            }
            UpdateWindows();
        }

        private void UpdateWindows()
        {
            Rotor[] rotors = enigma.GetAllRotors();
            for (int i = 0; i < 5; i++)
            {
                if (rotors[i] != null)
                {
                    rotorWindows[i] = IntToChar(rotors[i].CurrentPosition);
                }
                else
                {
                    rotorWindows[i] = ' ';
                }
            }
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RotorWindows"));
            }
        }
        private void UpdatePartNames()
        {
            Rotor[] rotors = enigma.GetAllRotors();
            for (int i = 0; i < 5; i++)
            {
                if (rotors[i] != null)
                {
                    rotorNames[i] = rotors[i].Name;
                }
                else
                {
                    rotorNames[i] = "Not Used";
                }
            }
            reflectorName = enigma.GetReflector().Name;
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RotorNames"));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ReflectorName"));
            }
        }
        private int CharToInt(char c)
        {
            return Array.FindIndex(convert, target => target == c);
        }
        private char IntToChar(int num)
        {
            return convert[num];
        }
    }
}