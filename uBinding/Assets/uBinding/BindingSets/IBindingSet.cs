using System;
using System.Linq.Expressions;
using uBinding.Collections;
using uBinding.Contexts;

namespace uBinding.BindingSets
{
    public interface IBindingSet
    {
        SourceBindingContext<TValue> Bind<TValue>(Expression<Func<TValue>> sourceExpression);
        CollectionBindingContext<T> Bind<T>(IReadonlyObservableCollection<T> collection);
    }
}