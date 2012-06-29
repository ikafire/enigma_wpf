using System;
using System.Linq;

namespace EnigmaWPF.Model
{
    /// <summary>
    /// 所有Rotor的父類別
    /// </summary>
    [Serializable]
    public class Rotor : IPart
    {
        protected Rotor()
        {
            this.CurrentPosition = 0;
            this.RingPosition = 0;
        }

        public Rotor(Rotor rot)
        {
            this.CurrentPosition = rot.CurrentPosition;
            this.Name = rot.Name;
            this.Notches = rot.Notches;
            this.RingPosition = rot.RingPosition;
            this.Wiring = rot.Wiring;
        }

        public int CurrentPosition { get; private set; }

        public int RingPosition { get; set; }

        public virtual string Name { get; set; }

        public virtual int[] Wiring { get; protected set; }

        public virtual int[] Notches { get; protected set; }

        internal bool IsNotch
        {
            get
            {
                if (this.Notches == null)
                {
                    return false;
                }

                if (this.Notches.Contains(this.CurrentPosition))
                {
                    return true;
                }

                return false;
            }
        }

        internal void Turn(TurningDirection direction)
        {
            switch (direction)
            {
                case TurningDirection.Forward:
                    this.CurrentPosition++;
                    this.CurrentPosition %= 26;
                    break;
                case TurningDirection.Reverse:
                    this.CurrentPosition--;
                    if (this.CurrentPosition < 0)
                    {
                        this.CurrentPosition += 26;
                    }

                    break;
            }
        }

        internal void ForwardInputSignal(int input, out int output)
        {
            int posFix = this.CurrentPosition + this.RingPosition;
            output = this.Wiring[(input + posFix) % 26] - posFix;
            if (output < 0)
            {
                output += 26;
            }
        }

        internal void ReverseInputSignal(int input, out int output)
        {
            int posFix = this.CurrentPosition + this.RingPosition;
            output = Array.FindIndex(this.Wiring, i => i == (input + posFix) % 26) - posFix;
            if (output < 0)
            {
                output += 26;
            }
        }

        internal void ResetPosition()
        {
            this.CurrentPosition = 0;
        }
    }
}
