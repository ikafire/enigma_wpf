using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enigma_WPF
{
    public class EnigmaMachine
    {
        //public EnigmaMachine()
        //{
        //    WorkingRotors = new Rotor[5] { Rotor.CreateRotor(PartType.RotorIII), Rotor.CreateRotor(PartType.RotorIV), Rotor.CreateRotor(PartType.RotorII), null, null };
        //    Reflector = Reflector.UKW_B();
        //}
        public EnigmaMachine(params Rotor[] rots)
        {
            if (rots.Length > 5)
            {
                throw new ArgumentOutOfRangeException("more than 5 rotor params when constructing EnigmaMachine");
            }
            WorkingRotors = new Rotor[5];
            for (int i = 0; i < 5; i++)
            {
                try { WorkingRotors[i] = rots[i]; }
                catch { WorkingRotors[i] = null; }
            }
            Reflector = Reflector.UKW_B();
        }
        public Reflector Reflector { get; set; }
        public Rotor[] WorkingRotors { get; set; }
        public Rotor GetRotor(int rotNum)
        {
            return WorkingRotors[rotNum];
        }
        public void InputSignal(int input, out int output)
        {
            TurnOver();
            MutateSignal(input, out output);
        }
        private void TurnOver()
        {
            if (WorkingRotors[1].isNotch)
            {
                WorkingRotors[0].ForwardTurn();
                WorkingRotors[1].ForwardTurn();
                WorkingRotors[2].ForwardTurn();
            }
            else if (WorkingRotors[0].isNotch)
            {
                WorkingRotors[0].ForwardTurn();
                WorkingRotors[1].ForwardTurn();
            }
            else
            {
                WorkingRotors[0].ForwardTurn();
            }
        }
        private void MutateSignal(int input, out int output)
        {
            int signal = input;
            WorkingRotors.ForEach(rot =>
                {
                    if (rot != null)
                        rot.ForwardInput(signal, out signal);
                });
            Reflector.Input(signal, out signal);
            WorkingRotors.ReverseForEach(rot =>
                {
                    if (rot != null) 
                        rot.ReverseInput(signal, out signal);
                });
            output = signal;
        }
    }
}
