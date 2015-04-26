using System;
using System.ComponentModel;
using uBinding.Binders;
using uBinding.Changables;
using uBinding.Core;

namespace uBinding.Descriptions
{
    public class MethodBindingDescription<TSourceValue> : IDescription
    {
        private readonly Action _action;
        private readonly string _propertyName;
        private readonly INotifyPropertyChanged _source;

        public MethodBindingDescription(INotifyPropertyChanged source, string propertyName, Action action)
        {
            _source = source;
            _propertyName = propertyName;
            _action = action;
        }

        public IBinder Apply()
        {
            var source = new BindableSource<TSourceValue>(_source, _propertyName, BindingMode.OneWay);
            return new MethodBinder(source, _action);
        }
    }
}