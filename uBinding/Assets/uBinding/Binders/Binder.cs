using uBinding.Changables;
using uBinding.Core;

namespace uBinding.Binders
{
    public abstract class Binder : IBinder
    {
        private readonly IChangable _source;
        private readonly bool _sourceIsUsed;
        private readonly IChangable _target;
        private readonly bool _targetIsUsed;
        private bool _isStopped;

        protected Binder(IChangable source, IChangable target, BindingMode mode)
        {
            _source = source;
            _target = target;
            _sourceIsUsed = mode == BindingMode.OneWay || mode == BindingMode.TwoWay;
            _targetIsUsed = mode == BindingMode.OneWayToSource || mode == BindingMode.TwoWay;
        }

        public void Start()
        {
            UpdateTarget();

            if (_sourceIsUsed)
                _source.ValueChanged += SourceOnValueChanged;

            if (_targetIsUsed)
                _target.ValueChanged += TargetOnValueChanged;
        }

        public void Stop()
        {
            if (_isStopped)
                return;

            if (_sourceIsUsed)
                _source.ValueChanged -= SourceOnValueChanged;

            if (_targetIsUsed)
                _target.ValueChanged -= TargetOnValueChanged;

            _isStopped = true;
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
}