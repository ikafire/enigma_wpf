using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enigma_WPF
{
    abstract class ElectricalPart
    {
        protected int[] wiring;
        public PartName Name { get; private set; }
    }
}