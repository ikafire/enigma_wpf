using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Media;
using System.Windows.Shapes;

namespace Enigma_WPF
{
    /// <summary>
    /// 獨立工具函式和擴充函式
    /// </summary>
    public static class Util
    {
        private static readonly char[] convert = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        
        public static int[] CloneArray(this int[] source)
        {
            if (source == null)
            {
                return null;
            }

            var clone = new int[source.Length];
            int i = 0;
            foreach (int content in source)
            {
                clone[i++] = content;
            }

            return clone;
        }
        
        public static List<T> ToListWithoutNull<T>(this T[] array)
        {
            var list = new List<T>();
            foreach (T t in array)
            {
                if (t != null)
                {
                    list.Add(t);
                }
            }

            return list;
        }
        
        public static void ForEach<T>(this ObservableCollection<T> collection, Action<T> action)
        {
            foreach (T content in collection)
            {
                action(content);
            }
        }
        
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            foreach (T content in array)
            {
                action(content);
            }
        }
        
        public static void ReverseForEach<T>(this T[] array, Action<T> action)
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                action(array[i]);
            }
        }
        
        public static void RemoveNull<T>(this ObservableCollection<T> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i] == null)
                {
                    collection.RemoveAt(i);
                }
            }
        }
        
        public static int GetRotorCount(this Rotor[] rotArray)
        {
            int num = 0;
            foreach (Rotor rot in rotArray)
            {
                if (rot == null)
                {
                    return num;
                }

                num++;
            }

            Debug.Assert(num == rotArray.Length);
            return num;
        }
        
        public static ObservableCollection<Rotor> Sorted(this ObservableCollection<Rotor> collection)
        {
            var list = new List<Rotor>(collection);
            list.Sort(Util.CompareRotors);
            return new ObservableCollection<Rotor>(list);
        }
        
        public static int CompareRotors(Rotor x, Rotor y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            int byType = x.Type.CompareTo(y.Type);
            if (byType != 0)
            {
                return byType;
            }

            return x.Name.CompareTo(y.Name);
        }
        
        public static void Ding()
        {
            SoundPlayer ding = new SoundPlayer();
            ding.Play();
        }
        
        public static int CharToInt(char c)
        {
            int i = Array.FindIndex(convert, target => target == c);
            if (i < 0)
            {
                throw new ArgumentOutOfRangeException("cannot find corresbonding char");
            }

            return i;
        }
        
        public static char IntToChar(int num)
        {
            return convert[num];
        }
        
        public static int TagAsInt(Shape source)
        {
            return int.Parse(source.Tag.ToString());
        }
    }
}
