using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using uBinding.Binders;
using uBinding.Collections;
using uBinding.Contexts;

namespace uBinding.BindingSets
{
    public class BindingSet : IBindingSet, IDisposable
    {
        private readonly List<IBinder> _binders = new List<IBinder>();
        private uint _addNumber;
        private uint _bindNumber;

        public SourceBindingContext<TValue> Bind<TValue>(Expression<Func<TValue>> sourceExpression)
        {
            ThrowIfNotAppliedBinder();

            var unaryExpression = sourceExpression.Body as UnaryExpression;

            var memberExpression = unaryExpression != null
                ? (MemberExpression) unaryExpression.Operand
                : (MemberExpression) sourceExpression.Body;

            var expression = memberExpression.Expression;
            var necessaryType = typeof (INotifyPropertyChanged);
            var providedType = expression.Type;

            if (necessaryType.IsAssignableFrom(providedType) == false)
            {
                var message = string.Format("{0} is not derived from {1}.", providedType.Name, necessaryType.Name);
                throw new InvalidOperationException(message);
            }

            var source = Expression.Lambda<Func<INotifyPropertyChanged>>(expression).Compile()();

            _bindNumber++;
            return new SourceBindingSetContext<TValue>(this, source, memberExpression.Member.Name);
        }

        public CollectionBindingContext<T> Bind<T>(IReadonlyObservableCollection<T> collection)
        {
            ThrowIfNotAppliedBinder();
            _bindNumber++;
            return new CollectionBindingSetContext<T>(this, collection);
        }

        public void Dispose()
        {
            foreach (var binder in _binders)
            {
                binder.Stop();
            }
        }

        private void ThrowIfNotAppliedBinder()
        {
            if (_bindNumber != _addNumber) throw new InvalidOperationException("There is not applied binder");
        }

        public void Add(IBinder binder)
        {
            _addNumber++;
            _binders.Add(binder);
        }
    }
}