using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace EnigmaWPF.Model
{
    /// <summary>
    /// class EnigmaMachine的操作者，所有對EnigmaMachine的動作都會經由此
    /// </summary>
    public class EnigmaOperator
    {
        private EnigmaMachine machine;

        public EnigmaOperator()
        {
            this.AllRotors = new List<Rotor> 
            {
                new RotorI(), new RotorII(), new RotorIII(), new RotorIV(), new RotorV(), new RotorVI(), new RotorVII(), new RotorVIII(), new RotorBeta(), new RotorGamma() 
            };

            this.AllReflectors = new List<Reflector>
            {
                new UKW_B(), new UKW_C(), new UKW_Thin_B(), new UKW_Thin_C()
            };

            this.machine = new EnigmaMachine
            {
                Rotors = new RotorSet(this.AllRotors[0], this.AllRotors[1], this.AllRotors[2]),
                Reflector = this.AllReflectors[0]
            };

            this.PartInfo = new EnigmaPartInfo(this.machine);
        }

        public EnigmaPartInfo PartInfo { get; private set; }

        public List<Rotor> AllRotors { get; private set; }

        public List<Reflector> AllReflectors { get; private set; }

        public List<Rotor> WorkingRotors
        {
            get { return this.machine.Rotors.ToList(); }
        }

        public Reflector WorkingReflector
        {
            get { return this.machine.Reflector; }
        }

        public void InputChar(char input, out char output)
        {
            int signal = Util.CharToInt(input);
            if (signal < 0)
            {
                output = input;
                return;
            }

            this.machine.InputSignal(signal, out signal);
            output = Util.IntToChar(signal);
            this.PartInfo.UpdateRotorWindows();
        }

        public void InputString(string input, out string output)
        {
            output = string.Empty;
            char outChar;
            foreach (char inChar in input.ToUpperInvariant())
            {
                this.InputChar(inChar, out outChar);
                output += outChar;
            }
        }

        public void TurnRotor(int rotNum, TurningDirection direction)
        {
            Rotor rot = this.machine.Rotors[rotNum];
            if (rot == null) 
            {
                return;
            }

            rot.Turn(direction);
            this.PartInfo.UpdateRotorWindows();
        }

        public void ResetRotorPosition()
        {
            foreach (Rotor rot in this.machine.Rotors.ToList())
            {
                rot.ResetPosition();
            }

            this.PartInfo.UpdateRotorWindows();
        }

        public void ChangeRotors(List<Rotor> newRots)
        {
            if (newRots.Count <= 0)
            {
                throw new ArgumentOutOfRangeException("There must be at least one working rotor.");
            }

            this.machine.Rotors = new RotorSet(newRots);
            this.PartInfo.UpdateRotorWindows();
            this.PartInfo.UpdatePartInfo();
        }

        public void ChangeReflector(Reflector newRef)
        {
            this.machine.Reflector = newRef;
            this.PartInfo.UpdatePartInfo();
        }
    }
}