﻿using System.Collections.Generic;

namespace Utils
{
    public static class EnumerableHelpers
    {
        public static void AddRange<T>(this IList<T> collection, IEnumerable<T> items)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
