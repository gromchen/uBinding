using System;
using System.ComponentModel;
using System.Reflection;
using uBinding.Core;

namespace uBinding.Changables
{
    public class BindableSource<TValue> : IBindable<TValue>
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly INotifyPropertyChanged _source;

        public BindableSource(INotifyPropertyChanged source, string propertyName, BindingMode mode)
        {
            _source = source;
            _propertyInfo = _source.GetType().GetProperty(propertyName);

            if (mode == BindingMode.TwoWay || mode == BindingMode.OneWay)
            {
                _source.PropertyChanged += SourceOnPropertyChanged;
            }
        }

        public TValue Value
        {
            get { return (TValue) _propertyInfo.GetValue(_source, null); }
            set { _propertyInfo.SetValue(_source, value, null); }
        }

        public event Action ValueChanged;

        private void SourceOnPropertyChanged(object sender, PropertyChangedEventArgs args)
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