using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Utils
{
    
    internal static class ObservableCollectionHelpers
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if(items == null)
                return;

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
