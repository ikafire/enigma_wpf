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
        private string[] rotorDescriptions = new string[5];  //WARINIG: DO NOT SET THIS VALUE DIRECTLY, USE UpdatePartNames()
        private string reflectorName;   //WARINIG: DO NOT SET THIS VALUE DIRECTLY, USE UpdatePartNames()
        private List<Rotor> allRotors;
        public event PropertyChangedEventHandler PropertyChanged;
        public char[] RotorWindows
        {
            get { return rotorWindows; }
        }
        public string[] RotorDescriptions
        {
            get { return rotorDescriptions; }
        }
        public string ReflectorName
        {
            get { return reflectorName; }
        }
        public List<Rotor> WorkingRotors
        {
            get { return enigma.WorkingRotors.ToListWithoutNull(); }
        }
        //public List<Rotor> AllRotors
        //{
        //    get { return new List<Rotor>(allRotors); }
        //}
        public List<Rotor> UnusedRotors
        {
            get
            {
                List<Rotor> unused = new List<Rotor>(allRotors);
                WorkingRotors.ForEach(rot => unused.Remove(rot));
                return unused;
            }
        }
        public EnigmaOperator()
        {
            allRotors = Rotor.CreateAllRotors();
            enigma = new EnigmaMachine(allRotors[0], allRotors[1], allRotors[2]);
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
        public void TurnRotor(int rotNum, TurningDirection direction)
        {
            Rotor rot = enigma.WorkingRotors[rotNum];
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
        //public void SetRotors(List<Rotor> allRots, params int[] workRotIndex)
        //{
        //    allRotors = allRots;
        //    if (workRotIndex.Length > 5 || workRotIndex.Length <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException("Working rotor count out of range");
        //    }
        //    List<Rotor> workRots = new List<Rotor>(workRotIndex.Length);
        //    workRotIndex.ForEach(i => workRots.Add(allRotors[i]));
        //    enigma.WorkingRotors = workRots.ToArray();
        //}
        public void ChangeRotors(List<Rotor> workRots, List<Rotor> unusedRots)
        {
            if (workRots.Count <= 0)
            {
                throw new ArgumentOutOfRangeException("There must be at least one working rotor.");
            }
            if (workRots.Count > 5)
            {
                throw new ArgumentOutOfRangeException("There can't be more than five working rotors.");
            }
            Rotor[] workRotArray = new Rotor[5];
            for (int i = 0; i < 5; i++)
            {
                try { workRotArray[i] = workRots[i]; }
                catch { workRotArray[i] = null; }
            }
            enigma.WorkingRotors = workRotArray;
            allRotors = unusedRots.Concat(workRots).ToList();
            UpdateWindows();
            UpdatePartNames();
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
                    rotorDescriptions[i] = string.Format("Rotor #{0}: {1}", i + 1, rotors[i].Name);
                }
                else
                {
                    rotorDescriptions[i] = string.Format("Rotor #{0}: Empty", i + 1);
                }
            }
            reflectorName = enigma.Reflector.Name;
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RotorDescriptions"));
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ReflectorName"));
            }
        }
    }
}