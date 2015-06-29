using System;
using System.ComponentModel;
using System.Linq.Expressions;
using uBinding.BindingSets;
using uBinding.Descriptions;

namespace uBinding.Contexts
{
    public class SourceBindingSetContext<TSourceValue> : SourceBindingContext<TSourceValue>
    {
        private readonly BindingSet _set;

        public SourceBindingSetContext(BindingSet set, INotifyPropertyChanged source, string propertyName)
            : base(source, propertyName)
        {
            _set = set;
        }

        public override BindingDescription<TSourceValue, TTargetValue> To<TTargetValue>(
            Expression<Func<TTargetValue>> targetExpression)
        {
            var description = base.To(targetExpression);
            description.Set = _set;
            return description;
        }

        public override MethodBindingDescription<TSourceValue> To(Action<TSourceValue> action)
        {
            var description = base.To(action);
            description.Set = _set;
            return description;
        }
    }
}