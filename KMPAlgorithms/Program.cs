using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace KMPAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = "BBC ABCDAB ABCDABCDABDE";
            var search = "ABCDABD";
            var kmp = new KMP();
            Debug.Assert(kmp.IsContainKMP(str, search) == kmp.IsContain(str, search));
            search = "ABCDD";
            Debug.Assert(kmp.IsContainKMP(str, search) == kmp.IsContain(str, search));
            search = "ABC";
            Debug.Assert(kmp.IsContainKMP(str, search) == kmp.IsContain(str, search));

            str = @"BBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABEBBC ABCDAB ABCDABCDABDE";
            search = "ABCDABD";
            var count = 10000;
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < count; i++)
            {
                kmp.IsContainKMP(str, search);
            }
            sw.Stop();
            Console.WriteLine($"KPM时间：{sw.ElapsedMilliseconds}");
            sw.Restart();
            for (int i = 0; i < count; i++)
            {
                kmp.IsContain(str, search);
            }
            sw.Stop();
            Console.WriteLine($"遍历时间：{sw.ElapsedMilliseconds}");
        }
    }

    public class KMP
    {
        private static Dictionary<string, int[]> PartTables = new Dictionary<string, int[]>();
        public bool IsContain(string str, string search)
        {
            var start = 0;
            var sameLength = 0;
            while (start + search.Length < str.Length)
            {
                if (sameLength == search.Length)
                {
                    return true;
                }
                if (str[start + sameLength] == search[sameLength])
                {
                    sameLength++;
                }
                else
                {
                    start++;
                    sameLength = 0;
                }
            }
            return false;
        }
        public bool IsContainKMP(string str, string search)
        {
            var start = 0;
            var sameLength = 0;
            if (!PartTables.ContainsKey(search))
            {
                var pt = new int[search.Length];
                for (int i = 1; i <= search.Length; i++)
                {
                    pt[i - 1] = GetPartLength(search[0..i]);
                }
                PartTables[search] = pt;
            }
            var partTable = PartTables[search];

            while (start + search.Length < str.Length)
            {
                if (sameLength == search.Length)
                {
                    return true;
                }
                if (str[start + sameLength] == search[sameLength])
                {
                    sameLength++;
                }
                else
                {
                    var step = 1;
                    if (sameLength > 0)
                    {
                        step = sameLength - partTable[sameLength - 1];
                    }

                    start += step;
                    sameLength = 0;
                }
            }
            return false;
        }

        public int GetPartLength(string str)
        {
            for (int i = str.Length - 1; i > 0; i--)
            {
                if (str[0..i] == str[^i..^0])
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
