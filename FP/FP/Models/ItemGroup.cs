using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FP.Models
{
    class ItemGroup<K, T>: ObservableCollection<T>
    {
        public K Key { get; private set; }
        public ItemGroup(K key, IEnumerable<T> items )
        {
            Key = key;

            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
