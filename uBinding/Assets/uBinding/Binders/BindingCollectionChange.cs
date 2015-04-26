using System.Collections.Generic;
using uBinding.Collections;

namespace uBinding.Binders
{
    public class BindingCollectionChange<T> : CollectionChange<T>
    {
        public BindingCollectionChange(IEnumerable<T> addedItems, bool isPopulation, params T[] removedItems)
            : base(addedItems, removedItems)
        {
            IsPopulation = isPopulation;
        }

        public bool IsPopulation { get; private set; }
    }
}