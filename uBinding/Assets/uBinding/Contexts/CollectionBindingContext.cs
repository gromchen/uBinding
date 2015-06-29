using System;
using uBinding.Binders;
using uBinding.Collections;
using uBinding.Descriptions;

namespace uBinding.Contexts
{
    public class CollectionBindingContext<T>
    {
        private readonly IReadonlyObservableCollection<T> _collection;

        protected CollectionBindingContext(IReadonlyObservableCollection<T> collection)
        {
            _collection = collection;
        }

        public virtual CollectionBindingDescription<T> To(Action<BindingCollectionChange<T>> action)
        {
            return new CollectionBindingDescription<T>(_collection, action);
        }
    }
}