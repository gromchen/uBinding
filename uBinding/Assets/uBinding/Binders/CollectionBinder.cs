using System;
using System.Linq;
using uBinding.Collections;

namespace uBinding.Binders
{
    public class CollectionBinder<T> : IBinder
    {
        private readonly IReadonlyObservableCollection<T> _collection;
        private readonly Action<BindingCollectionChange<T>> _updateAction;

        public CollectionBinder(IReadonlyObservableCollection<T> collection,
            Action<BindingCollectionChange<T>> updateAction)
        {
            _collection = collection;
            _updateAction = updateAction;
        }

        public void Dispose()
        {
            _collection.Changed -= CollectionOnChanged;
        }

        public void Start()
        {
            _updateAction(new BindingCollectionChange<T>(_collection, true));
            _collection.Changed += CollectionOnChanged;
        }

        private void CollectionOnChanged(CollectionChange<T> change)
        {
            _updateAction(new BindingCollectionChange<T>(change.AddedItems, false, change.RemovedItems.ToArray()));
        }
    }
}