using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnigmaWPF.Model
{
    public interface IPart
    {
        string Name { get; }
        int[] Wiring { get; }
        int[] Notches { get; }
    }
}
