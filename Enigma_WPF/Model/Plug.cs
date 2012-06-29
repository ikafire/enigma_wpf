using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnigmaWPF.Model
{
    [Serializable]
    public struct Plug
    {
        private int x;

        private int y;

        public Plug(int x, int y)
        {
            if (x < y)
            {
                this.x = x;
                this.y = y;
            }
            else if (x == y)
            {
                throw new ArgumentException("Cannot switch from one to itself");
            }
            else
            {
                this.y = x;
                this.x = y;
            }
        }

        public int X
        {
            get { return this.x; }
        }

        public int Y
        {
            get { return this.y; }
        }

        public int Mutate(int input)
        {
            if (this.X == input)
            {
                return this.Y;
            }
            else if (this.Y == input)
            {
                return this.X;
            }
            else
            {
                return input;
            }
        }

        public bool Contains(int num)
        {
            if (this.X == num || this.Y == num)
            {
                return true;
            }

            return false;
        }
    }
}
