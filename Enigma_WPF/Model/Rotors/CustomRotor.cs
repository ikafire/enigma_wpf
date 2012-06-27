using System.Linq;

namespace EnigmaWPF.Model
{
    /// <summary>
    /// 自定Rotor，可設定Name，Notches，Wiring
    /// </summary>
    public class CustomRotor : Rotor
    {
        private static int customRotCount = 0;

        public CustomRotor()
            : base()
        {
            base.Name = string.Format("Custom Rotor {0}", customRotCount);
            base.Wiring = null;
            base.Notches = null;
        }

        public static bool CheckWiring(int[] wiring)
        {
            if (wiring.Length != 26)
            {
                return false;
            }

            for (int i = 0; i < 26; i++)
            {
                if (!wiring.Contains(i))
                {
                    return false;
                }
            }

            return true;
        }

        public void SetWiring(int[] wiring)
        {
            base.Wiring = wiring;
        }

        public void SetNotches(int[] notches)
        {
            base.Notches = notches;
        }
    }
}
