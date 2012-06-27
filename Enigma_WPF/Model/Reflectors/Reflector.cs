using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnigmaWPF.Model
{
    /// <summary>
    /// 所有Reflector的父類別
    /// </summary>
    public abstract class Reflector
    {
        public virtual string Name { get; protected set; }

        public virtual int[] Wiring { get; protected set; }

        internal void InputSignal(int input, out int output)
        {
            output = this.Wiring[input];
        }
    }
}