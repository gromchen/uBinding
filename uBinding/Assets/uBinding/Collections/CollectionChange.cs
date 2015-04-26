using System.Collections.Generic;

namespace uBinding.Collections
{
    public class CollectionChange<T>
    {
        public CollectionChange(IEnumerable<T> addedItems, params T[] removedItems)
        {
            AddedItems = addedItems;
            RemovedItems = removedItems;
        }

        public IEnumerable<T> AddedItems { get; private set; }
        public IEnumerable<T> RemovedItems { get; private set; }
    }
}