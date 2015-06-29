using System;
using uBinding.Binders;
using uBinding.BindingSets;
using uBinding.Collections;
using uBinding.Descriptions;

namespace uBinding.Contexts
{
    public class CollectionBindingSetContext<T> : CollectionBindingContext<T>
    {
        private readonly BindingSet _set;

        public CollectionBindingSetContext(BindingSet set, IReadonlyObservableCollection<T> collection)
            : base(collection)
        {
            _set = set;
        }

        public override CollectionBindingDescription<T> To(Action<BindingCollectionChange<T>> action)
        {
            var description = base.To(action);
            description.Set = _set;
            return description;
        }
    }
}