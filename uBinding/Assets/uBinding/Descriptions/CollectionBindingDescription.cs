using System;
using uBinding.Binders;
using uBinding.Collections;

namespace uBinding.Descriptions
{
    public class CollectionBindingDescription<T> : IDescription
    {
        private readonly Action<BindingCollectionChange<T>> _action;
        private readonly IReadonlyObservableCollection<T> _collection;

        public CollectionBindingDescription(IReadonlyObservableCollection<T> collection,
            Action<BindingCollectionChange<T>> action)
        {
            _collection = collection;
            _action = action;
        }

        public IBinder Apply()
        {
            return new CollectionBinder<T>(_collection, _action);
        }
    }
}