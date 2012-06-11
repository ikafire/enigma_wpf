using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enigma_WPF
{
    class Rotor : ElectricalPart
    {
        private int[] notches;
        public int CurrentPosition { get; private set; }    //the value to show on window
        public int RingPosition { get; private set; }   //ring-wiring conformation
        private Rotor(int[] wiring, int[] notches, PartName type)
        {
            this.wiring = wiring;
            this.notches = notches;
            RingPosition = 0;
            CurrentPosition = 0;
        }
        public void ForwardTurn()
        {
            CurrentPosition++;
            CurrentPosition %= 26;
        }
        public void ReverseTurn()
        {
            CurrentPosition--;
            if (CurrentPosition < 0)
                CurrentPosition += 26;
        }
        public void ForwardInput(int input, out int output)
        {
            int posFix = CurrentPosition + RingPosition;
            output = wiring[(input + posFix) % 26] - posFix;
            if (output < 0) output += 26;
        }
        public void ReverseInput(int input, out int output)
        {
            int posFix = CurrentPosition + RingPosition;
            output = Array.FindIndex(wiring, i => i == (input + posFix) % 26) - posFix;
            if (output < 0) output += 26;
        }
        public bool isNotch
        {
            get
            {
                foreach (int notch in notches)
                {
                    if (CurrentPosition == notch)
                        return true;
                }
                return false;
            }
        }
        public void ResetPosition()
        {
            CurrentPosition = 0;
        }
        public void ResetRing()
        {
            RingPosition = 0;
        }
        public void SetRing(int pos)
        {
            if (pos < 0 || pos >= 26) throw new ArgumentOutOfRangeException("Ring position out of range");
            RingPosition = pos;
        }
        public static Rotor CreateRotor(PartName name)
        {
            int[] wiring, notch;
            switch (name)
            {
                case PartName.RotorI:
                    //EKMFLGDQVZNTOWYHXUSPAIBRCJ
                    wiring = new int[26] { 4, 10, 12, 5, 11, 6, 3, 16, 21, 25, 13, 19, 14, 22, 24, 7, 23, 20, 18, 15, 0, 8, 1, 17, 2, 9 };
                    //Q
                    notch = new int[1] { 16 };
                    break;
                case PartName.RotorII:
                    //AJDKSIRUXBLHWTMCQGZNPYFVOE
                    wiring = new int[26] { 0, 9, 3, 10, 18, 8, 17, 20, 23, 1, 11, 7, 22, 19, 12, 2, 16, 6, 25, 13, 15, 24, 5, 21, 14, 4 };
                    //E
                    notch = new int[1] { 4 };
                    break;
                case PartName.RotorIII:
                    //BDFHJLCPRTXVZNYEIWGAKMUSQO
                    wiring = new int[26] { 1, 3, 5, 7, 9, 11, 2, 15, 17, 19, 23, 21, 25, 13, 24, 4, 8, 22, 6, 0, 10, 12, 20, 18, 16, 14 };
                    //V
                    notch = new int[1] { 21 };
                    break;
                default:
                    throw new ArgumentException("Invalid RotorType");
            }
            return new Rotor(wiring, notch, name);
        }
    }
}
