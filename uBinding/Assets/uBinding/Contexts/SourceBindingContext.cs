using System;
using System.ComponentModel;
using System.Linq.Expressions;
using uBinding.BindingSets;
using uBinding.Descriptions;

namespace uBinding.Contexts
{
    public class SourceBindingContext<TSourceValue>
    {
        private readonly string _propertyName;
        private readonly IBindingSet _set;
        private readonly INotifyPropertyChanged _source;

        public SourceBindingContext(IBindingSet set, INotifyPropertyChanged source, string propertyName)
        {
            _set = set;
            _source = source;
            _propertyName = propertyName;
        }

        public BindingDescription<TSourceValue, TTargetValue> To<TTargetValue>(
            Expression<Func<TTargetValue>> targetExpression)
        {
            var unaryExpression = targetExpression.Body as UnaryExpression;

            var memberExpression = unaryExpression != null
                ? (MemberExpression) unaryExpression.Operand
                : (MemberExpression) targetExpression.Body;

            var target = Expression.Lambda<Func<object>>(memberExpression.Expression).Compile()();

            var description = new BindingDescription<TSourceValue, TTargetValue>(_source, _propertyName, target,
                memberExpression.Member.Name);
            _set.Add(description);
            return description;
        }

        public void To(Action action)
        {
            _set.Add(new MethodBindingDescription<TSourceValue>(_source, _propertyName, action));
        }
    }
}