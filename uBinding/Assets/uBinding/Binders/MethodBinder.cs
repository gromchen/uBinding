using System;
using uBinding.Changables;
using uBinding.Core;

namespace uBinding.Binders
{
    /// <summary>
    ///     todo: use <see cref="IBinder" />
    /// </summary>
    public class MethodBinder<TValue> : Binder
    {
        private readonly IBindable<TValue> _source;
        private readonly Action<TValue> _action;

        public MethodBinder(IBindable<TValue> source, Action<TValue> action)
            : base(source, new EmptyChangable(), BindingMode.OneWay)
        {
            _source = source;
            _action = action;
        }

        protected override void UpdateTarget()
        {
            _action(_source.Value);
        }

        protected override void UpdateSource()
        {
            // source is not updated
        }
    }
}