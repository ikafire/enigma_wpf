namespace EnigmaWPF.Model
{
    /// <summary>
    /// Rotor type I
    /// Wiring: EKMFLGDQVZNTOWYHXUSPAIBRCJ
    /// Notch: Q
    /// </summary>
    public sealed class RotorI : Rotor
    {
        internal RotorI()
            : base()
        {
            base.Name = "RotorI";
            base.Wiring = new int[26] { 4, 10, 12, 5, 11, 6, 3, 16, 21, 25, 13, 19, 14, 22, 24, 7, 23, 20, 18, 15, 0, 8, 1, 17, 2, 9 };
            base.Notches = new int[1] { 16 };
        }
    }

    /// <summary>
    /// Rotor type II
    /// Wiring: AJDKSIRUXBLHWTMCQGZNPYFVOE
    /// Notch: E
    /// </summary>
    public sealed class RotorII : Rotor
    {
        internal RotorII()
            : base()
        {
            base.Name = "RotorII";
            base.Wiring = new int[26] { 0, 9, 3, 10, 18, 8, 17, 20, 23, 1, 11, 7, 22, 19, 12, 2, 16, 6, 25, 13, 15, 24, 5, 21, 14, 4 };
            base.Notches = new int[1] { 4 };
        }
    }

    /// <summary>
    /// Rotor type III
    /// Wiring: BDFHJLCPRTXVZNYEIWGAKMUSQO
    /// Notch: V
    /// </summary>
    public sealed class RotorIII : Rotor
    {
        internal RotorIII()
            : base()
        {
            base.Name = "RotorIII";
            base.Wiring = new int[26] { 1, 3, 5, 7, 9, 11, 2, 15, 17, 19, 23, 21, 25, 13, 24, 4, 8, 22, 6, 0, 10, 12, 20, 18, 16, 14 };
            base.Notches = new int[1] { 21 };
        }
    }

    /// <summary>
    /// Rotor type IV
    /// Wiring: ESOVPZJAYQUIRHXLNFTGKDCMWB
    /// Notch: J
    /// </summary>
    public sealed class RotorIV : Rotor
    {
        internal RotorIV()
            : base()
        {
            base.Name = "RotorIV";
            base.Wiring = new int[26] { 4, 18, 14, 21, 15, 25, 9, 0, 24, 16, 20, 8, 17, 7, 23, 11, 13, 5, 19, 6, 10, 3, 2, 12, 22, 1 };
            base.Notches = new int[1] { 9 };
        }
    }

    /// <summary>
    /// Rotor type V
    /// Wiring: VZBRGITYUPSDNHLXAWMJQOFECK
    /// Notch: Z
    /// </summary>
    public sealed class RotorV : Rotor
    {
        internal RotorV()
            : base()
        {
            base.Name = "RotorV";
            base.Wiring = new int[26] { 21, 25, 1, 17, 6, 8, 19, 24, 20, 15, 18, 3, 13, 7, 11, 23, 0, 22, 12, 9, 16, 14, 5, 4, 2, 10 };
            base.Notches = new int[1] { 25 };
        }
    }

    /// <summary>
    /// Rotor type VI
    /// Wiring: JPGVOUMFYQBENHZRDKASXLICTW
    /// Notch: MZ
    /// </summary>
    public sealed class RotorVI : Rotor
    {
        internal RotorVI()
            : base()
        {
            base.Name = "RotorVI";
            base.Wiring = new int[26] { 9, 15, 6, 21, 14, 20, 12, 5, 24, 16, 1, 4, 13, 7, 25, 17, 3, 10, 0, 18, 23, 11, 8, 2, 19, 22 };
            base.Notches = new int[2] { 12, 25 };
        }
    }

    /// <summary>
    /// Rotor type VII
    /// Wiring: NZJHGRCXMYSWBOUFAIVLPEKQDT
    /// Notch: MZ
    /// </summary>
    public sealed class RotorVII : Rotor
    {
        internal RotorVII()
            : base()
        {
            base.Name = "RotorVII";
            base.Wiring = new int[26] { 13, 25, 9, 7, 6, 17, 2, 23, 12, 24, 18, 22, 1, 14, 20, 5, 0, 8, 21, 11, 15, 4, 10, 16, 3, 19 };
            base.Notches = new int[2] { 12, 25 };
        }
    }

    /// <summary>
    /// Rotor type VIII
    /// Wiring: FKQHTLXOCBJSPDZRAMEWNIUYGV
    /// Notch: MZ
    /// </summary>
    public sealed class RotorVIII : Rotor
    {
        internal RotorVIII()
            : base()
        {
            base.Name = "RotorVIII";
            base.Wiring = new int[26] { 5, 10, 16, 7, 19, 11, 23, 14, 2, 1, 9, 18, 15, 3, 25, 17, 0, 12, 4, 22, 13, 8, 20, 24, 6, 21 };
            base.Notches = new int[2] { 12, 25 };
        }
    }

    /// <summary>
    /// Rotor type Beta
    /// Wiring: LEYJVCNIXWPBQMDRTAKZGFUHOS
    /// Notch: N/A
    /// </summary>
    public sealed class RotorBeta : Rotor
    {
        internal RotorBeta()
            : base()
        {
            base.Name = "RotorBeta";
            base.Wiring = new int[26] { 11, 4, 24, 9, 21, 2, 13, 8, 23, 22, 15, 1, 16, 12, 3, 17, 19, 0, 10, 25, 6, 5, 20, 7, 14, 18 };
            base.Notches = null;
        }
    }

    /// <summary>
    /// Rotor type Gamma
    /// Wiring: FSOKANUERHMBTIYCWLQPZXVGJD
    /// Notch: N/A
    /// </summary>
    public sealed class RotorGamma : Rotor
    {
        internal RotorGamma()
            : base()
        {
            base.Name = "RotorGamma";
            base.Wiring = new int[26] { 5, 18, 14, 10, 0, 13, 20, 4, 17, 7, 12, 1, 19, 8, 24, 2, 22, 11, 16, 15, 25, 23, 21, 6, 9, 3 };
            base.Notches = null;
        }
    }
}
