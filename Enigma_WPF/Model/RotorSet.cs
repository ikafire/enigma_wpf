using System.Collections.Generic;
using System;

namespace EnigmaWPF.Model
{
    /// <summary>
    /// 由大於等於一個Rotor組成的Rotor set
    /// </summary>
    internal class RotorSet
    {
        List<Rotor> rotSet;

        public RotorSet(params Rotor[] rots)
        {
            this.rotSet = new List<Rotor>(rots);
        }

        public RotorSet(List<Rotor> rotList)
        {
            this.rotSet = rotList;
        }

        public Rotor this[int rotNum]
        {
            get
            {
                if (rotNum >= this.rotSet.Count)
                {
                    return null;
                }

                return this.rotSet[rotNum];
            }
        }

        public int Count
        {
            get
            {
                return this.rotSet.Count;
            }
        }

        public void ForwardInputSignal(int input, out int output)
        {
            int signal = input;
            this.rotSet.ForEach(rot => rot.ForwardInputSignal(signal, out signal));
            output = signal;
        }

        public void ReverseInputSignal(int input, out int output)
        {
            int signal = input;
            for (int i = this.rotSet.Count - 1; i >= 0; i--)
            {
                this.rotSet[i].ReverseInputSignal(signal, out signal);
            }
            output = signal;
        }

        public void TurnOver()
        {
            if (this.rotSet.Count >= 3)
            {
                if (this.rotSet[1].IsNotch)
                {
                    this.rotSet[0].Turn(TurningDirection.Forward);
                    this.rotSet[1].Turn(TurningDirection.Forward);
                    this.rotSet[2].Turn(TurningDirection.Forward);
                    return;
                }
            }

            if (this.rotSet.Count >= 2)
            {
                if (this.rotSet[0].IsNotch)
                {
                    this.rotSet[0].Turn(TurningDirection.Forward);
                    this.rotSet[1].Turn(TurningDirection.Forward);
                    return;
                }
            }

            this.rotSet[0].Turn(TurningDirection.Forward);
        }

        public List<Rotor> ToList()
        {
            return new List<Rotor>(this.rotSet);
        }

        public void ForEach(Action<Rotor> action)
        {
            foreach (Rotor rot in this.rotSet)
            {
                action(rot);
            }
        }
    }
}
