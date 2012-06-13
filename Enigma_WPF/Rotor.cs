using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enigma_WPF
{
    public class Rotor : ElectricalPart
    {
        private int[] notches;
        public int CurrentPosition { get; private set; }    //the value to show on window
        public int RingPosition { get; set; }   //ring-wiring conformation
        public int[] Notches
        {
            get { return notches.CloneArray(); }
            set
            {
                if (this.Type != PartType.CustomRotor)
                {
                    throw new ArgumentException("Cannot set notches for non-custom rotor");
                }
                notches = value;
            }
        }
        public int[] Wiring
        {
            get { return wiring.CloneArray(); }
            set
            {
                if (this.Type != PartType.CustomRotor)
                {
                    throw new ArgumentException("Cannot set wiring for non-custom rotor");
                }
                wiring = value;
            }
        }
        private Rotor()
        {
            this.CurrentPosition = 0;
            this.RingPosition = 0;
        }
        private Rotor(int[] wiring, int[] notches, PartType type)
        {
            this.wiring = wiring;
            this.notches = notches;
            RingPosition = 0;
            CurrentPosition = 0;
        }
        public Rotor(Rotor target)
        {
            if (target == null) return;
            this.Name = target.Name;
            this.Type = target.Type;
            this.wiring = target.wiring.CloneArray();
            this.notches = target.notches.CloneArray();
            this.RingPosition = target.RingPosition;
            this.CurrentPosition = 0;
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
                if (notches.Contains(CurrentPosition)) return true;
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
        public static Rotor CreateRotor(PartType type)
        {
            int[] wiring, notch;
            switch (type)
            {
                case PartType.RotorI:
                    //EKMFLGDQVZNTOWYHXUSPAIBRCJ
                    wiring = new int[26] { 4, 10, 12, 5, 11, 6, 3, 16, 21, 25, 13, 19, 14, 22, 24, 7, 23, 20, 18, 15, 0, 8, 1, 17, 2, 9 };
                    //Q
                    notch = new int[1] { 16 };
                    break;
                case PartType.RotorII:
                    //AJDKSIRUXBLHWTMCQGZNPYFVOE
                    wiring = new int[26] { 0, 9, 3, 10, 18, 8, 17, 20, 23, 1, 11, 7, 22, 19, 12, 2, 16, 6, 25, 13, 15, 24, 5, 21, 14, 4 };
                    //E
                    notch = new int[1] { 4 };
                    break;
                case PartType.RotorIII:
                    //BDFHJLCPRTXVZNYEIWGAKMUSQO
                    wiring = new int[26] { 1, 3, 5, 7, 9, 11, 2, 15, 17, 19, 23, 21, 25, 13, 24, 4, 8, 22, 6, 0, 10, 12, 20, 18, 16, 14 };
                    //V
                    notch = new int[1] { 21 };
                    break;
                case PartType.RotorIV:
                    //ESOVPZJAYQUIRHXLNFTGKDCMWB
                    wiring = new int[26] { 4, 18, 14, 21, 15, 25, 9, 0, 24, 16, 20, 8, 17, 7, 23, 11, 13, 5, 19, 6, 10, 3, 2, 12, 22, 1 };
                    //J
                    notch = new int[1] { 9 };
                    break;
                case PartType.RotorV:
                    //VZBRGITYUPSDNHLXAWMJQOFECK
                    wiring = new int[26] { 21, 25, 1, 17, 6, 8, 19, 24, 20, 15, 18, 3, 13, 7, 11, 23, 0, 22, 12, 9, 16, 14, 5, 4, 2, 10 };
                    //Z
                    notch = new int[1] { 25 };
                    break;
                case PartType.RotorVI:
                    //JPGVOUMFYQBENHZRDKASXLICTW
                    wiring = new int[26] { 9, 15, 6, 21, 14, 20, 12, 5, 24, 16, 1, 4, 13, 7, 25, 17, 3, 10, 0, 18, 23, 11, 8, 2, 19, 22 };
                    //MZ
                    notch = new int[2] { 12, 25 };
                    break;
                case PartType.RotorVII:
                    //NZJHGRCXMYSWBOUFAIVLPEKQDT
                    wiring = new int[26] { 13, 25, 9, 7, 6, 17, 2, 23, 12, 24, 18, 22, 1, 14, 20, 5, 0, 8, 21, 11, 15, 4, 10, 16, 3, 19 };
                    //MZ
                    notch = new int[2] { 12, 25 };
                    break;
                case PartType.RotorVIII:
                    //FKQHTLXOCBJSPDZRAMEWNIUYGV
                    wiring = new int[26] { 5, 10, 16, 7, 19, 11, 23, 14, 2, 1, 9, 18, 15, 3, 25, 17, 0, 12, 4, 22, 13, 8, 20, 24, 6, 21 };
                    //MZ
                    notch = new int[2] { 12, 25 };
                    break;
                case PartType.Rotor_Beta:
                    //LEYJVCNIXWPBQMDRTAKZGFUHOS
                    wiring = new int[26] { 11, 4, 24, 9, 21, 2, 13, 8, 23, 22, 15, 1, 16, 12, 3, 17, 19, 0, 10, 25, 6, 5, 20, 7, 14, 18 };
                    //no notch
                    notch = null;
                    break;
                case PartType.Rotor_Gamma:
                    //FSOKANUERHMBTIYCWLQPZXVGJD
                    wiring = new int[26] { 5, 18, 14, 10, 0, 13, 20, 4, 17, 7, 12, 1, 19, 8, 24, 2, 22, 11, 16, 15, 25, 23, 21, 6, 9, 3 };
                    //no notch
                    notch = null;
                    break;
                default:
                    throw new ArgumentException("Invalid RotorType");
            }
            Rotor rotor = new Rotor(wiring, notch, type);
            rotor.Name = type.ToString();
            return rotor;
        }
        public static Rotor CustomRotor()
        {
            Rotor customRotor = new Rotor();
            customRotor.Type = PartType.CustomRotor;
            customRotor.Name = "custom";
            return customRotor;
        }
        public static List<Rotor> CreateAllRotors()
        {
            List<Rotor> allRots = new List<Rotor>();
            foreach (PartType rotType in Enum.GetValues(typeof(PartType)))
            {
                try
                {
                    allRots.Add(Rotor.CreateRotor(rotType));
                }
                catch { }
            }
            return allRots;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Rotor) || this.Type == PartType.CustomRotor) return false;
            Rotor target = (Rotor)obj;
            return this.Type == target.Type;
        }
    }
}
