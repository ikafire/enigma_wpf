using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnigmaWPF.Model
{
    /// <summary>
    /// UKW B
    /// Wiring: YRUHQ SLDPX NGOKM IEBFZ CWVJAT
    /// </summary>
    public sealed class UKW_B : Reflector
    {
        internal UKW_B()
        {
            base.Name = "UKW B";
            base.Wiring = new int[26] { 24, 17, 20, 7, 16, 18, 11, 3, 15, 23, 13, 6, 14, 10, 12, 8, 4, 1, 5, 25, 2, 22, 21, 9, 0, 19 };
        }
    }

    /// <summary>
    /// UKW C
    /// Wiring: FVPJI AOYED RZXWG CTKUQ SBNMHL
    /// </summary>
    public sealed class UKW_C : Reflector
    {
        internal UKW_C()
        {
            base.Name = "UKW C";
            base.Wiring = new int[26] { 5, 21, 15, 9, 8, 0, 14, 24, 4, 3, 17, 25, 23, 22, 6, 2, 19, 10, 20, 16, 18, 1, 13, 12, 7, 11 };
        }
    }

    /// <summary>
    /// UKW Thin B
    /// Wiring: ENKQA UYWJI COPBL MDXZV FTHRGS
    /// </summary>
    public sealed class UKW_Thin_B : Reflector
    {
        internal UKW_Thin_B()
        {
            base.Name = "UKW Thin B";
            base.Wiring = new int[26] { 4, 13, 10, 16, 0, 20, 24, 22, 9, 8, 2, 14, 15, 1, 11, 12, 3, 23, 25, 21, 5, 19, 7, 17, 6, 18 };
        }
    }

    /// <summary>
    /// UKW Thin C
    /// Wiring: RDOBJ NTKVE HMLFC WZAXG YIPSUQ
    /// </summary>
    public sealed class UKW_Thin_C : Reflector
    {
        internal UKW_Thin_C()
        {
            base.Name = "UKW Thin C";
            base.Wiring = new int[26] { 17, 3, 14, 1, 9, 13, 19, 10, 21, 4, 7, 12, 11, 5, 2, 22, 25, 0, 23, 6, 24, 8, 15, 18, 20, 16 };
        }
    }
}
