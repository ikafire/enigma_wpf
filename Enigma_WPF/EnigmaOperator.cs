using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Enigma_WPF
{
    public class EnigmaOperator : INotifyPropertyChanged
    {
        private EnigmaMachine enigma;
        private char[] rotorWindows = new char[5];    //WARNING: DO NOT SET THIS VALUE DIRECTLY, USE UpdateWindows()
        private string[] rotorNames = new string[5];  //WARINIG: DO NOT SET THIS VALUE DIRECTLY, USE UpdatePartNames()
        private string reflectorName;   //WARINIG: DO NOT SET THIS VALUE DIRECTLY, USE UpdatePartNames()
        private List<Rotor> availableRotors;
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
        public List<Rotor> WorkingRotors
        {
            get { return enigma.WorkingRotors.ToListWithoutNull().DeepCopy(); }
        }
        public List<Rotor> AllRotors
        {
            get { return availableRotors.DeepCopy(); }
        }
        public EnigmaOperator()
        {
            availableRotors = Rotor.CreateAllRotors();
            enigma = new EnigmaMachine(availableRotors[0], availableRotors[1], availableRotors[2]);
            UpdateWindows();
            UpdatePartNames();
        }
        public void InputChar(char input, out char output)
        {
            int signal = Util.CharToInt(input);
            enigma.InputSignal(signal, out signal);
            output = Util.IntToChar(signal);
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
            Rotor[] rots = enigma.WorkingRotors;
            foreach (Rotor rot in rots)
            {
                if (rot != null)
                {
                    rot.ResetPosition();
                }
            }
            UpdateWindows();
        }
        public void SetRotors(List<Rotor> allRots, params int[] workRotIndex)
        {
            availableRotors = allRots;
            if (workRotIndex.Length > 5 || workRotIndex.Length <= 0)
            {
                throw new ArgumentOutOfRangeException("Working rotor count out of range");
            }
            List<Rotor> workRots = new List<Rotor>(workRotIndex.Length);
            workRotIndex.ForEach(i => workRots.Add(availableRotors[i]));
            enigma.WorkingRotors = workRots.ToArray();
        }

        private void UpdateWindows()
        {
            Rotor[] rotors = enigma.WorkingRotors;
            for (int i = 0; i < 5; i++)
            {
                if (rotors[i] != null)
                {
                    rotorWindows[i] = Util.IntToChar(rotors[i].CurrentPosition);
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
            Rotor[] rotors = enigma.WorkingRotors;
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
            reflectorName = enigma.Reflector.Name;
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RotorNames"));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ReflectorName"));
            }
        }
    }
}