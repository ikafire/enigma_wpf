using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enigma_WPF
{
    class EnigmaMachine
    {
        private Rotor[] workingRotors;
        private Reflector reflector;
        public EnigmaMachine()
        {
            workingRotors = new Rotor[3] { Rotor.CreateRotor(PartName.RotorI), Rotor.CreateRotor(PartName.RotorII), Rotor.CreateRotor(PartName.RotorIII) };
            reflector = Reflector.UKW_B();
        }
        public Rotor GetRotor(int rotNum)
        {
            return workingRotors[rotNum];
        }
        public Rotor[] GetAllRotors()
        {
            return workingRotors;
        }
        public void SetRotor(int rotNum, Rotor newRotor)
        {
            workingRotors[rotNum] = newRotor;
        }
        public void SetReflector(Reflector newReflector)
        {
            this.reflector = newReflector;
        }
        public void InputSignal(int input, out int output)
        {
            TurnOver();
            MutateSignal(input, out output);
        }
        public void ManuallyTurn(int rotNum, TurningDirection direction)
        {
            switch (direction)
            {
                case TurningDirection.Forward:
                    workingRotors[rotNum].ForwardTurn();
                    break;
                case TurningDirection.Reverse:
                    workingRotors[rotNum].ReverseTurn();
                    break;
            }
        }
        private void TurnOver()
        {
            if (workingRotors[1].isNotch)
            {
                workingRotors[0].ForwardTurn();
                workingRotors[1].ForwardTurn();
                workingRotors[2].ForwardTurn();
            }
            else if (workingRotors[0].isNotch)
            {
                workingRotors[0].ForwardTurn();
                workingRotors[1].ForwardTurn();
            }
            else
            {
                workingRotors[0].ForwardTurn();
            }
        }
        private void MutateSignal(int input, out int output)
        {
            int signal = input;
            for (int i = 0; i < workingRotors.Length; i++)
            {
                workingRotors[i].ForwardInput(signal, out signal);
            }
            reflector.Input(signal, out signal);
            for (int i = workingRotors.Length - 1; i >= 0; i--)
            {
                workingRotors[i].ReverseInput(signal, out signal);
            }
            output = signal;
        }

    }
}
