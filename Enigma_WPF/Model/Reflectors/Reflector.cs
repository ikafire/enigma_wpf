using System;

namespace EnigmaWPF.Model
{
    /// <summary>
    /// 所有Reflector的父類別
    /// </summary>
    [Serializable]
    public class Reflector
    {
        public virtual string Name { get; set; }

        public virtual int[] Wiring { get; protected set; }

        protected Reflector()
        {
        }

        public Reflector(Reflector reflector)
        {
            this.Name = reflector.Name;
            this.Wiring = reflector.Wiring;
        }

        internal void InputSignal(int input, out int output)
        {
            output = this.Wiring[input];
        }
    }
}