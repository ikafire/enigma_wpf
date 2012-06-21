using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Enigma_WPF
{
    /// <summary>
    /// class EnigmaMachine的操作者，所有對EnigmaMachine的動作都會經由此
    /// </summary>
    public class EnigmaOperator : INotifyPropertyChanged
    {
        private EnigmaMachine enigma;

        private char[] rotorWindows = new char[5]; // WARNING: DO NOT SET THIS VALUE DIRECTLY, USE UpdateWindows()

        private string[] rotorDescriptions = new string[5]; // WARINIG: DO NOT SET THIS VALUE DIRECTLY, USE UpdatePartNames()

        private string reflectorName; // WARINIG: DO NOT SET THIS VALUE DIRECTLY, USE UpdatePartNames()

        private List<Rotor> allRotors;

        public EnigmaOperator()
        {
            this.allRotors = Rotor.CreateAllRotors();
            this.enigma = new EnigmaMachine(this.allRotors[0], this.allRotors[1], this.allRotors[2]);
            this.UpdateWindows();
            this.UpdatePartNames();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public char[] RotorWindows
        {
            get { return this.rotorWindows; }
        }

        public string[] RotorDescriptions
        {
            get { return this.rotorDescriptions; }
        }

        public string ReflectorName
        {
            get { return this.reflectorName; }
        }

        public List<Rotor> WorkingRotors
        {
            get { return this.enigma.WorkingRotors.ToListWithoutNull(); }
        }

        public List<Rotor> UnusedRotors
        {
            get
            {
                List<Rotor> unused = new List<Rotor>(this.allRotors);
                this.WorkingRotors.ForEach(rot => unused.Remove(rot));
                return unused;
            }
        }

        public void InputChar(char input, out char output)
        {
            int signal = Util.CharToInt(input);
            this.enigma.InputSignal(signal, out signal);
            output = Util.IntToChar(signal);
            this.UpdateWindows();
        }

        public void TurnRotor(int rotNum, TurningDirection direction)
        {
            Rotor rot = this.enigma.WorkingRotors[rotNum];
            if (rot == null) 
            {
                return;
            }

            switch (direction)
            {
                case TurningDirection.Forward:
                    rot.ForwardTurn();
                    break;
                case TurningDirection.Reverse:
                    rot.ReverseTurn();
                    break;
            }

            this.UpdateWindows();
        }

        public void ResetRotorPosition()
        {
            Rotor[] rots = this.enigma.WorkingRotors;
            foreach (Rotor rot in rots)
            {
                if (rot != null)
                {
                    rot.ResetPosition();
                }
            }

            this.UpdateWindows();
        }

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
                try 
                { 
                    workRotArray[i] = workRots[i];
                }
                catch
                {
                    workRotArray[i] = null;
                }
            }

            this.enigma.WorkingRotors = workRotArray;
            this.allRotors = unusedRots.Concat(workRots).ToList();
            this.UpdateWindows();
            this.UpdatePartNames();
        }

        private void UpdateWindows()
        {
            Rotor[] rotors = this.enigma.WorkingRotors;
            for (int i = 0; i < 5; i++)
            {
                if (rotors[i] != null)
                {
                    this.rotorWindows[i] = Util.IntToChar(rotors[i].CurrentPosition);
                }
                else
                {
                    this.rotorWindows[i] = ' ';
                }
            }

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RotorWindows"));
            }
        }

        private void UpdatePartNames()
        {
            Rotor[] rotors = this.enigma.WorkingRotors;
            for (int i = 0; i < 5; i++)
            {
                if (rotors[i] != null)
                {
                    this.rotorDescriptions[i] = string.Format("Rotor #{0}: {1}", i + 1, rotors[i].Name);
                }
                else
                {
                    this.rotorDescriptions[i] = string.Format("Rotor #{0}: Empty", i + 1);
                }
            }

            this.reflectorName = this.enigma.Reflector.Name;
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RotorDescriptions"));
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ReflectorName"));
            }
        }
    }
}