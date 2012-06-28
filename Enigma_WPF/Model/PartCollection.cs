using System;
using System.Collections.Generic;

namespace EnigmaWPF.Model
{
    [Serializable]
    public class PartCollection
    {
        public List<Rotor> Rotors { get; set; }

        public Reflector Reflector { get; set; }

        public PlugBoard PlugBoard { get; set; }
    }
}
