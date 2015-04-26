using uBinding.Changables;
using uBinding.Converters;
using uBinding.Core;

namespace uBinding.Binders
{
    public abstract class Binder : IBinder
    {
        private readonly IChangable _source;
        private readonly bool _sourceIsUsed;
        private readonly IChangable _target;
        private readonly bool _targetIsUsed;

        protected Binder(IChangable source, IChangable target, BindingMode mode)
        {
            _source = source;
            _target = target;
            _sourceIsUsed = mode == BindingMode.OneWay || mode == BindingMode.TwoWay;
            _targetIsUsed = mode == BindingMode.OneWayToSource || mode == BindingMode.TwoWay;
        }

        public void Dispose()
        {
            // todo: implement dispose with finalization method

            if (_sourceIsUsed)
                _source.ValueChanged -= SourceOnValueChanged;

            if (_targetIsUsed)
                _target.ValueChanged -= TargetOnValueChanged;
        }

        public void Start()
        {
            UpdateTarget();

            if (_sourceIsUsed)
                _source.ValueChanged += SourceOnValueChanged;

            if (_targetIsUsed)
                _target.ValueChanged += TargetOnValueChanged;
        }

        protected abstract void UpdateTarget();
        protected abstract void UpdateSource();

        private void SourceOnValueChanged()
        {
            if (_targetIsUsed)
                _target.ValueChanged -= TargetOnValueChanged;

            UpdateTarget();

            if (_targetIsUsed)
                _target.ValueChanged += TargetOnValueChanged;
        }

        private void TargetOnValueChanged()
        {
            if (_sourceIsUsed)
                _source.ValueChanged -= SourceOnValueChanged;

            UpdateSource();

            if (_sourceIsUsed)
                _source.ValueChanged += SourceOnValueChanged;
        }
    }

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