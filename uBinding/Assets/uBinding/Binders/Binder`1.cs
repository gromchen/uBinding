using uBinding.Changables;
using uBinding.Core;

namespace uBinding.Binders
{
    public class Binder<TValue> : Binder
    {
        private readonly IBindable<TValue> _source;
        private readonly IBindable<TValue> _target;

        public Binder(IBindable<TValue> source, IBindable<TValue> target, BindingMode mode)
            : base(source, target, mode)
        {
            _source = source;
            _target = target;
        }

        protected override void UpdateTarget()
        {
            _target.Value = _source.Value;
        }

        protected override void UpdateSource()
        {
            _source.Value = _target.Value;
        }
    }
}