using System;
using System.ComponentModel;
using System.Linq.Expressions;
using uBinding.Descriptions;

namespace uBinding.Contexts
{
    public class SourceBindingContext<TSourceValue>
    {
        private readonly string _propertyName;
        private readonly INotifyPropertyChanged _source;

        public SourceBindingContext(INotifyPropertyChanged source, string propertyName)
        {
            _source = source;
            _propertyName = propertyName;
        }

        public virtual BindingDescription<TSourceValue, TTargetValue> To<TTargetValue>(
            Expression<Func<TTargetValue>> targetExpression)
        {
            var unaryExpression = targetExpression.Body as UnaryExpression;

            var memberExpression = unaryExpression != null
                ? (MemberExpression) unaryExpression.Operand
                : (MemberExpression) targetExpression.Body;

            var target = Expression.Lambda<Func<object>>(memberExpression.Expression).Compile()();

            return new BindingDescription<TSourceValue, TTargetValue>(_source, _propertyName, target,
                memberExpression.Member.Name);
        }

        public virtual MethodBindingDescription<TSourceValue> To(Action<TSourceValue> action)
        {
            return new MethodBindingDescription<TSourceValue>(_source, _propertyName, action);
        }
    }
}