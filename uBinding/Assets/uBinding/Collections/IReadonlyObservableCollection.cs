using System;
using System.Collections.Generic;

namespace uBinding.Collections
{
    public interface IReadonlyObservableCollection<T> : IEnumerable<T>
    {
        event Action<CollectionChange<T>> Changed;
    }
}