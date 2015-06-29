using System;
using uBinding.Binders;
using uBinding.BindingSets;
using uBinding.Collections;

namespace uBinding.Descriptions
{
    public class CollectionBindingDescription<T>
    {
        private readonly Action<BindingCollectionChange<T>> _action;
        private readonly IReadonlyObservableCollection<T> _collection;

        public CollectionBindingDescription(IReadonlyObservableCollection<T> collection,
            Action<BindingCollectionChange<T>> action)
        {
            _collection = collection;
            _action = action;
        }

        public BindingSet Set { private get; set; }

        public IBinder Apply()
        {
            var binder = new CollectionBinder<T>(_collection, _action);

            if (Set == null) throw new InvalidOperationException("Binding set is not set");
                
            Set.Add(binder);
            binder.Start();
            return binder;
        }
    }
}