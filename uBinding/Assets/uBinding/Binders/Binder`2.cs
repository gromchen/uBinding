using uBinding.Changables;
using uBinding.Converters;
using uBinding.Core;

namespace uBinding.Binders
{
    public class Binder<TSourceValue, TTargetValue> : Binder
    {
        private readonly IConverter<TSourceValue, TTargetValue> _converter;
        private readonly IBindable<TSourceValue> _source;
        private readonly IBindable<TTargetValue> _target;

        public Binder(IBindable<TSourceValue> source, IBindable<TTargetValue> target,
            IConverter<TSourceValue, TTargetValue> converter, BindingMode mode)
            : base(source, target, mode)
        {
            _source = source;
            _target = target;
            _converter = converter;
        }

        protected override void UpdateTarget()
        {
            _target.Value = _converter.ConvertBack(_source.Value);
        }

        protected override void UpdateSource()
        {
            _source.Value = _converter.Convert(_target.Value);
        }
    }
}