namespace EnigmaWPF.Model
{
    /// <summary>
    /// Enigma machine本體，負責協調裡面的零件調用以變異訊號
    /// </summary>
    internal class EnigmaMachine
    {
        public RotorSet Rotors { get; set; }

        public Reflector Reflector { get; set; }

        public PlugBoard PlugBoard { get; set; }

        public void InputSignal(int input, out int output)
        {
            int signal = input;
            this.Rotors.TurnOver();
            this.PlugBoard.MutateSignal(signal, out signal);
            this.Rotors.ForwardInputSignal(signal, out signal);
            this.Reflector.InputSignal(signal, out signal);
            this.Rotors.ReverseInputSignal(signal, out signal);
            this.PlugBoard.MutateSignal(signal, out signal);
            output = signal;
        }
    }
}