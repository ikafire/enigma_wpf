namespace Enigma_WPF
{
    public class Reflector : ElectricalPart
    {
        private Reflector(int[] wiring, PartType type)
        {
            this.wiring = wiring;
            this.Type = type;
            this.Name = type.ToString();
        }

        public static Reflector UKW_B()
        {
            // YRUHQSLDPXNGOKMIEBFZCWVJAT
            int[] wiring = new int[26] { 24, 17, 20, 7, 16, 18, 11, 3, 15, 23, 13, 6, 14, 10, 12, 8, 4, 1, 5, 25, 2, 22, 21, 9, 0, 19 };
            return new Reflector(wiring, PartType.UKW_B);
        }

        public void Input(int input, out int output)
        {
            output = this.wiring[input % 26];
        }
    }
}
