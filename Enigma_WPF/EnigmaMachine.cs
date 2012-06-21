using System;

namespace Enigma_WPF
{
    /// <summary>
    /// Enigma machine本體，負責協調裡面的零件調用以變異訊號
    /// </summary>
    public class EnigmaMachine
    {
        public EnigmaMachine(params Rotor[] rots)
        {
            if (rots.Length > 5)
            {
                throw new ArgumentOutOfRangeException("more than 5 rotor params when constructing EnigmaMachine");
            }

            this.WorkingRotors = new Rotor[5];
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    this.WorkingRotors[i] = rots[i];
                }
                catch 
                {
                    this.WorkingRotors[i] = null; 
                }
            }

            this.Reflector = Reflector.UKW_B();
        }

        public Reflector Reflector { get; set; }

        public Rotor[] WorkingRotors { get; set; }

        public void InputSignal(int input, out int output)
        {
            this.TurnOver();
            this.MutateSignal(input, out output);
        }

        private void TurnOver()
        {
            if (this.WorkingRotors.GetRotorCount() >= 3)
            {
                if (this.WorkingRotors[1].IsNotch)
                {
                    this.WorkingRotors[0].ForwardTurn();
                    this.WorkingRotors[1].ForwardTurn();
                    this.WorkingRotors[2].ForwardTurn();
                    return;
                }
            }

            if (this.WorkingRotors.GetRotorCount() >= 2)
            {
                if (this.WorkingRotors[0].IsNotch)
                {
                    this.WorkingRotors[0].ForwardTurn();
                    this.WorkingRotors[1].ForwardTurn();
                    return;
                }
            }

            this.WorkingRotors[0].ForwardTurn();
        }

        private void MutateSignal(int input, out int output)
        {
            int signal = input;
            this.WorkingRotors.ForEach(rot =>
                {
                    if (rot != null)
                    {
                        rot.ForwardInput(signal, out signal);
                    }
                });
            Reflector.Input(signal, out signal);
            this.WorkingRotors.ReverseForEach(rot =>
                {
                    if (rot != null)
                    {
                        rot.ReverseInput(signal, out signal);
                    }
                });
            output = signal;
        }
    }
}
