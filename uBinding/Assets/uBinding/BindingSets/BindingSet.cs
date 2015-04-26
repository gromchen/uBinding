using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using uBinding.Collections;
using uBinding.Contexts;
using uBinding.Descriptions;

namespace uBinding.BindingSets
{
    public class BindingSet : IBindingSet, IDisposable
    {
        private readonly List<IDisposable> _binders = new List<IDisposable>();
        private readonly List<IDescription> _descriptions = new List<IDescription>();

        public void Add(IDescription description)
        {
            _descriptions.Add(description);
        }

        public void Dispose()
        {
            foreach (var binder in _binders)
            {
                binder.Dispose();
            }
        }

        public SourceBindingContext<TValue> Bind<TValue>(Expression<Func<TValue>> sourceExpression)
        {
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

            return new SourceBindingContext<TValue>(this, source, memberExpression.Member.Name);
        }

        public CollectionBindingContext<T> Bind<T>(IReadonlyObservableCollection<T> collection)
        {
            return new CollectionBindingContext<T>(this, collection);
        }

        public void Apply()
        {
            foreach (var description in _descriptions)
            {
                var binder = description.Apply();
                binder.Start();
                _binders.Add(binder);
            }

            _descriptions.Clear();
        }
    }
}