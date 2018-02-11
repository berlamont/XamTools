using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoTouchx.Extensions
{
    public class Grouping<T,K> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public Grouping(IEnumerable<T> items, K key)
        {
            Key = key;
            Items.AddRange(items);
        }
    }
}
