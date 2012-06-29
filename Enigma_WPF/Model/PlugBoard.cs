using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnigmaWPF.Model
{
    [Serializable]
    public class PlugBoard
    {
        private List<Plug> plugs;

        public PlugBoard()
        {
            this.plugs = new List<Plug>();
        }

        public List<Plug> Plugs
        {
            get { return this.plugs; }
        }

        public void MutateSignal(int input, out int output)
        {
            output = input;
            foreach (var plug in plugs)
            {
                if (plug.Contains(input))
                {
                    output = plug.Mutate(input);
                }
            }
        }

        public bool Plug(int index1, int index2)
        {
            if (this.IsPlugged(index1, index2))
            {
                return false;
            }

            this.plugs.Add(new Plug(index1, index2));
            return true;
        }

        public bool UnPlug(int index1, int index2)
        {
            return this.plugs.Remove(new Plug(index1, index2));
        }

        public void UnPlugAll()
        {
            this.plugs = new List<Plug>();
        }

        private bool IsPlugged(int index1, int index2)
        {
            foreach (var plug in plugs)
            {
                if (plug.Contains(index1) || plug.Contains(index2))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
