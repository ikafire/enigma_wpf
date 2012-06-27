using System.ComponentModel;

namespace EnigmaWPF.Model
{
    /// <summary>
    /// EnigmaMachine的零件資訊窗口
    /// </summary>
    public class EnigmaPartInfo : INotifyPropertyChanged
    {
        private EnigmaMachine machine;

        internal EnigmaPartInfo(EnigmaMachine machine)
        {
            this.machine = machine;
            int rotCount = this.machine.Rotors.Count;
            this.RotorWindows = new char[5];
            this.RotorDescriptions = new string[5];
            this.UpdatePartInfo();
            this.UpdateRotorWindows();
        }

        public char[] RotorWindows { get; private set; }

        public string[] RotorDescriptions { get; private set; }

        public string ReflectorName { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateRotorWindows()
        {
            for (int i = 0; i < this.RotorWindows.Length; i++)
            {
                Rotor rot = this.machine.Rotors[i];
                if (rot != null)
                {
                    this.RotorWindows[i] = Util.IntToChar(rot.CurrentPosition);
                }
                else
                {
                    this.RotorWindows[i] = ' ';
                }
            }

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RotorWindows"));
            }
        }

        public void UpdatePartInfo()
        {
            for (int i = 0; i < this.RotorWindows.Length; i++)
            {
                Rotor rot = this.machine.Rotors[i];
                if (rot != null)
                {
                    this.RotorDescriptions[i] = string.Format("Rotor #{0}: {1}", i + 1, rot.Name);
                }
                else
                {
                    this.RotorDescriptions[i] = string.Format("Rotor #{0}: Empty", i + 1);
                }
            }

            this.ReflectorName = this.machine.Reflector.Name;
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RotorDescriptions"));
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ReflectorName"));
            }
        }

    }
}
