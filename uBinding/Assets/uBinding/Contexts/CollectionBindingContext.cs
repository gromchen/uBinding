using System;
using uBinding.Binders;
using uBinding.BindingSets;
using uBinding.Collections;
using uBinding.Descriptions;

namespace uBinding.Contexts
{
    public class CollectionBindingContext<T>
    {
        private readonly IReadonlyObservableCollection<T> _collection;
        private readonly IBindingSet _set;

        public CollectionBindingContext(IBindingSet set, IReadonlyObservableCollection<T> collection)
        {
            _set = set;
            _collection = collection;
        }

        public void To(Action<BindingCollectionChange<T>> action)
        {
            _set.Add(new CollectionBindingDescription<T>(_collection, action));
        }
    }
}