using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moovies.Helper
{
    public static class DictionaryHelper
    {
        public static void AddOrUpdateCount(this Dictionary<string, int> dictionary,
                                            string value)
        {
            int count = 0;
            dictionary.TryGetValue(value, out count);
            if (count < 1)
            {
                dictionary.Add(value, 1);
            }
            else
            {
                dictionary[value] = count + 1;
            }
        }

        public static void AddFormat<TKey>(this Dictionary<TKey, int> dictionary,
                TKey key,
                string formatString,
                params object[] argList)
        {
            //dictionary.Add(key, string.Format(formatString, argList));
        }
    }
}