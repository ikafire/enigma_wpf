using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enigma_WPF
{
    class Reflector : ElectricalPart
    {
        private Reflector(int[] wiring, PartType type)
        {
            this.wiring = wiring;
            this.Type = type;
        }

        public void Input(int input, out int output)
        {
            output = wiring[input % 26];
        }
        public static Reflector UKW_B()
        {
            //YRUHQSLDPXNGOKMIEBFZCWVJAT
            int[] wiring = new int[26] { 24, 17, 20, 7, 16, 18, 11, 3, 15, 23, 13, 6, 14, 10, 12, 8, 4, 1, 5, 25, 2, 22, 21, 9, 0, 19 };
            Reflector ukw = new Reflector(wiring, PartType.UKW_B);
            ukw.Name = ukw.Type.ToString();
            return ukw;
        }
    }
}
