using System;
using System.Collections.Generic;
using System.Linq;

namespace Enigma_WPF
{
    /// <summary>
    /// Enigma machine中的rotor零件，可轉動及變異訊號
    /// </summary>
    public class Rotor : ElectricalPart
    {
        private static int numOfCustomRot = 0;

        private int[] notches;

        public Rotor(Rotor target)
        {
            if (target == null)
            {
                return;
            }

            this.Name = target.Name;
            this.Type = target.Type;
            this.wiring = target.wiring.CloneArray();
            this.notches = target.notches.CloneArray();
            this.RingPosition = target.RingPosition;
            this.CurrentPosition = 0;
        }

        private Rotor()
        {
        }

        private Rotor(int[] wiring, int[] notches, PartType type)
        {
            this.wiring = wiring;
            this.notches = notches;
            this.Name = type.ToString();
            this.RingPosition = 0;
            this.CurrentPosition = 0;
        }

        public int CurrentPosition { get; private set; }    // the value to show on window

        public int RingPosition { get; set; }   // ring-wiring conformation

        public int[] Notches
        {
            get
            {
                return this.notches.CloneArray();
            }

            set
            {
                if (this.Type != PartType.CustomRotor)
                {
                    throw new ArgumentException("Cannot set notches for non-custom rotor");
                }

                this.notches = value;
            }
        }

        public int[] Wiring
        {
            get
            {
                return this.wiring.CloneArray();
            }

            set
            {
                if (this.Type != PartType.CustomRotor)
                {
                    throw new ArgumentException("Cannot set wiring for non-custom rotor");
                }

                this.wiring = value;
            }
        }

        public bool IsNotch
        {
            get
            {
                if (this.notches == null)
                {
                    return false;
                }

                if (this.notches.Contains(this.CurrentPosition))
                {
                    return true;
                }

                return false;
            }
        }

        public static Rotor CustomRotor()
        {
            Rotor customRot = new Rotor();
            customRot.Type = PartType.CustomRotor;
            customRot.Name = string.Format("CustomRotor{0}", ++numOfCustomRot);
            customRot.Notches = null;
            customRot.Wiring = null;
            customRot.RingPosition = 0;
            customRot.CurrentPosition = 0;
            return customRot;
        }

        public static List<Rotor> CreateAllRotors()
        {
            var rotList = new List<Rotor>()
            {
                // Wiring: EKMFLGDQVZNTOWYHXUSPAIBRCJ
                // Notch: Q
                new Rotor(
                    wiring: new int[26] { 4, 10, 12, 5, 11, 6, 3, 16, 21, 25, 13, 19, 14, 22, 24, 7, 23, 20, 18, 15, 0, 8, 1, 17, 2, 9 },
                    notches: new int[1] { 16 },
                    type: PartType.RotorI),

                // Wiring: AJDKSIRUXBLHWTMCQGZNPYFVOE
                // Notch: E
                new Rotor(
                    wiring: new int[26] { 0, 9, 3, 10, 18, 8, 17, 20, 23, 1, 11, 7, 22, 19, 12, 2, 16, 6, 25, 13, 15, 24, 5, 21, 14, 4 },
                    notches: new int[1] { 4 },
                    type: PartType.RotorII),

                // Wiring: BDFHJLCPRTXVZNYEIWGAKMUSQO
                // Notch: V
                new Rotor(
                    wiring: new int[26] { 1, 3, 5, 7, 9, 11, 2, 15, 17, 19, 23, 21, 25, 13, 24, 4, 8, 22, 6, 0, 10, 12, 20, 18, 16, 14 },
                    notches: new int[1] { 21 },
                    type: PartType.RotorIII),

                // Wiring: ESOVPZJAYQUIRHXLNFTGKDCMWB
                // Notch: J
                new Rotor(
                    wiring: new int[26] { 4, 18, 14, 21, 15, 25, 9, 0, 24, 16, 20, 8, 17, 7, 23, 11, 13, 5, 19, 6, 10, 3, 2, 12, 22, 1 },
                    notches: new int[1] { 9 },
                    type: PartType.RotorIV),

               // Wiring: VZBRGITYUPSDNHLXAWMJQOFECK
               // Notch: Z
               new Rotor(
                    wiring: new int[26] { 21, 25, 1, 17, 6, 8, 19, 24, 20, 15, 18, 3, 13, 7, 11, 23, 0, 22, 12, 9, 16, 14, 5, 4, 2, 10 },
                    notches: new int[1] { 25 },
                    type: PartType.RotorV),

                // Wiring: JPGVOUMFYQBENHZRDKASXLICTW
                // Notch: MZ
                new Rotor(
                    wiring: new int[26] { 9, 15, 6, 21, 14, 20, 12, 5, 24, 16, 1, 4, 13, 7, 25, 17, 3, 10, 0, 18, 23, 11, 8, 2, 19, 22 },
                    notches: new int[2] { 12, 25 },
                    type: PartType.RotorVI),

                // Wiring: NZJHGRCXMYSWBOUFAIVLPEKQDT
                // Notch: MZ
                new Rotor(
                    wiring: new int[26] { 13, 25, 9, 7, 6, 17, 2, 23, 12, 24, 18, 22, 1, 14, 20, 5, 0, 8, 21, 11, 15, 4, 10, 16, 3, 19 },
                    notches: new int[2] { 12, 25 },
                    type: PartType.RotorVII),

                // Wiring: FKQHTLXOCBJSPDZRAMEWNIUYGV
                // Notch: MZ
                new Rotor(
                    wiring: new int[26] { 5, 10, 16, 7, 19, 11, 23, 14, 2, 1, 9, 18, 15, 3, 25, 17, 0, 12, 4, 22, 13, 8, 20, 24, 6, 21 },
                    notches: new int[2] { 12, 25 },
                    type: PartType.RotorVIII),

                // Wiring: LEYJVCNIXWPBQMDRTAKZGFUHOS
                // Notch: null
                new Rotor(
                    wiring: new int[26] { 11, 4, 24, 9, 21, 2, 13, 8, 23, 22, 15, 1, 16, 12, 3, 17, 19, 0, 10, 25, 6, 5, 20, 7, 14, 18  },
                    notches: null,
                    type: PartType.Rotor_Beta),

                // Wiring: FSOKANUERHMBTIYCWLQPZXVGJD
                // Notch: null
                new Rotor(
                    wiring: new int[26] { 5, 18, 14, 10, 0, 13, 20, 4, 17, 7, 12, 1, 19, 8, 24, 2, 22, 11, 16, 15, 25, 23, 21, 6, 9, 3  },
                    notches: null,
                    type: PartType.Rotor_Gamma),
            };
            return rotList;
        }

        public void ForwardTurn()
        {
            this.CurrentPosition++;
            this.CurrentPosition %= 26;
        }

        public void ReverseTurn()
        {
            this.CurrentPosition--;
            if (this.CurrentPosition < 0)
            {
                this.CurrentPosition += 26;
            }
        }

        public void ForwardInput(int input, out int output)
        {
            int posFix = this.CurrentPosition + this.RingPosition;
            output = this.wiring[(input + posFix) % 26] - posFix;
            if (output < 0)
            {
                output += 26;
            }
        }

        public void ReverseInput(int input, out int output)
        {
            int posFix = this.CurrentPosition + this.RingPosition;
            output = Array.FindIndex(this.wiring, i => i == (input + posFix) % 26) - posFix;
            if (output < 0)
            {
                output += 26;
            }
        }

        public void ResetPosition()
        {
            this.CurrentPosition = 0;
        }

        public void ResetRing()
        {
            this.RingPosition = 0;
        }

        public void SetRing(int pos)
        {
            if (pos < 0 || pos >= 26)
            {
                throw new ArgumentOutOfRangeException("Ring position out of range");
            }

            this.RingPosition = pos;
        }

        ////private static Rotor OfficialRotor(PartType type)
        ////{
        ////    //int[] wiring, notch;
        ////    //switch (type)
        ////    //{
        ////    //    case PartType.RotorI:
        ////    //        // Wiring: EKMFLGDQVZNTOWYHXUSPAIBRCJ
        ////    //        // Notch: Q
        ////    //        wiring = new int[26] { 4, 10, 12, 5, 11, 6, 3, 16, 21, 25, 13, 19, 14, 22, 24, 7, 23, 20, 18, 15, 0, 8, 1, 17, 2, 9 };
        ////    //        notch = new int[1] { 16 };
        ////    //        break;
        ////    //    case PartType.RotorII:
        ////    //        // Wiring: AJDKSIRUXBLHWTMCQGZNPYFVOE
        ////    //        // Notch: E
        ////    //        wiring = new int[26] { 0, 9, 3, 10, 18, 8, 17, 20, 23, 1, 11, 7, 22, 19, 12, 2, 16, 6, 25, 13, 15, 24, 5, 21, 14, 4 };
        ////    //        notch = new int[1] { 4 };
        ////    //        break;
        ////    //    case PartType.RotorIII:
        ////    //        // Wiring: BDFHJLCPRTXVZNYEIWGAKMUSQO
        ////    //        // Notch: V
        ////    //        wiring = new int[26] { 1, 3, 5, 7, 9, 11, 2, 15, 17, 19, 23, 21, 25, 13, 24, 4, 8, 22, 6, 0, 10, 12, 20, 18, 16, 14 };
        ////    //        notch = new int[1] { 21 };
        ////    //        break;
        ////    //    case PartType.RotorIV:
        ////    //        // Wiring: ESOVPZJAYQUIRHXLNFTGKDCMWB
        ////    //        // Notch: J
        ////    //        wiring = new int[26] { 4, 18, 14, 21, 15, 25, 9, 0, 24, 16, 20, 8, 17, 7, 23, 11, 13, 5, 19, 6, 10, 3, 2, 12, 22, 1 };
        ////    //        notch = new int[1] { 9 };
        ////    //        break;
        ////    //    case PartType.RotorV:
        ////    //        // Wiring: VZBRGITYUPSDNHLXAWMJQOFECK
        ////    //        // Notch: Z
        ////    //        wiring = new int[26] { 21, 25, 1, 17, 6, 8, 19, 24, 20, 15, 18, 3, 13, 7, 11, 23, 0, 22, 12, 9, 16, 14, 5, 4, 2, 10 };
        ////    //        notch = new int[1] { 25 };
        ////    //        break;
        ////    //    case PartType.RotorVI:
        ////    //        // Wiring: JPGVOUMFYQBENHZRDKASXLICTW
        ////    //        // Notch: MZ
        ////    //        wiring = new int[26] { 9, 15, 6, 21, 14, 20, 12, 5, 24, 16, 1, 4, 13, 7, 25, 17, 3, 10, 0, 18, 23, 11, 8, 2, 19, 22 };
        ////    //        notch = new int[2] { 12, 25 };
        ////    //        break;
        ////    //    case PartType.RotorVII:
        ////    //        // Wiring: NZJHGRCXMYSWBOUFAIVLPEKQDT
        ////    //        // Notch: MZ
        ////    //        wiring = new int[26] { 13, 25, 9, 7, 6, 17, 2, 23, 12, 24, 18, 22, 1, 14, 20, 5, 0, 8, 21, 11, 15, 4, 10, 16, 3, 19 };
        ////    //        notch = new int[2] { 12, 25 };
        ////    //        break;
        ////    //    case PartType.RotorVIII:
        ////    //        // Wiring: FKQHTLXOCBJSPDZRAMEWNIUYGV
        ////    //        // Notch: MZ
        ////    //        wiring = new int[26] { 5, 10, 16, 7, 19, 11, 23, 14, 2, 1, 9, 18, 15, 3, 25, 17, 0, 12, 4, 22, 13, 8, 20, 24, 6, 21 };
        ////    //        notch = new int[2] { 12, 25 };
        ////    //        break;
        ////    //    case PartType.Rotor_Beta:
        ////    //        // Wiring: LEYJVCNIXWPBQMDRTAKZGFUHOS
        ////    //        // Notch: Null
        ////    //        wiring = new int[26] { 11, 4, 24, 9, 21, 2, 13, 8, 23, 22, 15, 1, 16, 12, 3, 17, 19, 0, 10, 25, 6, 5, 20, 7, 14, 18 };
        ////    //        notch = null;
        ////    //        break;
        ////    //    case PartType.Rotor_Gamma:
        ////    //        // Wiring: FSOKANUERHMBTIYCWLQPZXVGJD
        ////    //        // Notch: Null
        ////    //        wiring = new int[26] { 5, 18, 14, 10, 0, 13, 20, 4, 17, 7, 12, 1, 19, 8, 24, 2, 22, 11, 16, 15, 25, 23, 21, 6, 9, 3 };
        ////    //        notch = null;
        ////    //        break;
        ////    //    default:
        ////    //        throw new ArgumentException("Invalid RotorType");
        ////    //}

        ////    //Rotor rotor = new Rotor(wiring, notch, type);
        ////    //return rotor;
        ////}
    }
}
