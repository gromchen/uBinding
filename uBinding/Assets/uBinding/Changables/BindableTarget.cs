using System;
using System.ComponentModel;
using System.Reflection;
using uBinding.Core;

namespace uBinding.Changables
{
    public class BindableTarget<TValue> : IBindable<TValue>
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly object _target;

        public BindableTarget(object target, string propertyName, object raiserObject, BindingMode mode)
        {
            _target = target;
            _propertyInfo = _target.GetType().GetProperty(propertyName);

            string message;

            if (mode == BindingMode.OneWay || mode == BindingMode.OneTime)
            {
                if (raiserObject == null)
                    return;

                message = string.Format("Raiser is redundant if mode is {0}", mode);
                throw new InvalidOperationException(message);
            }

            var notifingTarget = _target as INotifyPropertyChanged;

            if (notifingTarget != null)
            {
                if (raiserObject != null)
                {
                    message = string.Format("Raiser is redundant if target is {0}", target.GetType().Name);
                    throw new InvalidOperationException(message);
                }

                notifingTarget.PropertyChanged += NotifingTargetOnPropertyChanged;
                return;
            }

            var raiser = raiserObject as IRaisable;

            if (raiser != null)
            {
                raiser.Raised += OnValueChanged;
                return;
            }

            var valueRaiser = raiserObject as IRaisable;

            if (valueRaiser != null)
            {
                valueRaiser.Raised += OnValueChanged;
                return;
            }

            message = string.Format("You need to specify raiser if mode is {0}.", mode);
            throw new InvalidOperationException(message);
        }

        public event Action ValueChanged;

        public TValue Value
        {
            get { return (TValue) _propertyInfo.GetValue(_target, null); }
            set { _propertyInfo.SetValue(_target, value, null); }
        }

        private void NotifingTargetOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == _propertyInfo.Name)
            {
                OnValueChanged();
            }
        }

        protected virtual void OnValueChanged()
        {
            var handler = ValueChanged;
            if (handler != null) handler();
        }
    }
}