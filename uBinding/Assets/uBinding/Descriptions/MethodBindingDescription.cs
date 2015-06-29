using System;
using System.ComponentModel;
using uBinding.Binders;
using uBinding.BindingSets;
using uBinding.Changables;
using uBinding.Core;

namespace uBinding.Descriptions
{
    public class MethodBindingDescription<TSourceValue>
    {
        private readonly Action<TSourceValue> _action;
        private readonly string _propertyName;
        private readonly INotifyPropertyChanged _source;

        public MethodBindingDescription(INotifyPropertyChanged source, string propertyName, Action<TSourceValue> action)
        {
            _source = source;
            _propertyName = propertyName;
            _action = action;
        }

        public BindingSet Set { private get; set; }

        public IBinder Apply()
        {
            var source = new BindableSource<TSourceValue>(_source, _propertyName, BindingMode.OneWay);
            var binder = new MethodBinder<TSourceValue>(source, _action);

            if (Set == null) throw new InvalidOperationException("Binding set is not set");
                
            Set.Add(binder);
            binder.Start();
            return binder;
        }
    }
}