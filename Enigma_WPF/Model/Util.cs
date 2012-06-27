using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Media;
using System.Windows.Shapes;

namespace EnigmaWPF.Model
{
    /// <summary>
    /// 獨立工具函式和擴充函式
    /// </summary>
    public static class Util
    {
        private static readonly char[] convert = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        static SoundPlayer ding = new SoundPlayer();

        public static void Ding()
        {
            ding.Play();
        }
        
        public static int CharToInt(char c)
        {
            return Array.FindIndex(convert, target => target == c);
        }
        
        public static char IntToChar(int num)
        {
            return convert[num];
        }
    }
}
