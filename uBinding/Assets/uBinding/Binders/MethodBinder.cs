using System;
using uBinding.Changables;
using uBinding.Core;

namespace uBinding.Binders
{
    /// <summary>
    ///     todo: use <see cref="IBinder" />
    /// </summary>
    public class MethodBinder : Binder
    {
        private readonly Action _action;

        public MethodBinder(IChangable source, Action action)
            : base(source, new EmptyChangable(), BindingMode.OneWay)
        {
            _action = action;
        }

        protected override void UpdateTarget()
        {
            _action();
        }

        protected override void UpdateSource()
        {
            // source is not updated
        }
    }
}