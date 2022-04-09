using System;
using System.Collections.Generic;
using System.Text;

namespace FreeloadMap.Lib.Utility
{
    public class StringEx
    {
#warning 移到OurOpenSource里
        public static string CatStrings(IEnumerable<string> strings)
        {
            int count = 0;
            foreach(string str in strings)
            {
                count += str.Length;
            }

            char[] r = new char[count];
            count = 0;
            foreach (string str in strings)
            {
                Array.Copy(str.ToCharArray(), 0, r, count, str.Length);
                count += str.Length;
            }

            return new string(r);
        }
        //public static string CatStrings(IEnumerable<string> strings)
        //{
        //    StringBuilder r = new StringBuilder();
        //
        //    foreach (string str in strings)
        //    {
        //        r.Append(str);
        //    }
        //
        //    return r.ToString();
        //}
    }
}
