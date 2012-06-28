using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnigmaWPF.Model
{
    [Serializable]
    public class PlugBoard
    {
        public int[] Wiring { get; private set; }

        public PlugBoard()
        {
            this.Wiring = new int[26] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
        }

        public void ForwardInputSignal(int input, out int output)
        {
            output = this.Wiring[input];
        }

        public void ReverseInputSignal(int input, out int output)
        {
            output = Array.FindIndex(this.Wiring, i => i == input);
        }
    }
}
