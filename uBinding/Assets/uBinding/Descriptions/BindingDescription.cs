using System;
using System.ComponentModel;
using uBinding.Binders;
using uBinding.BindingSets;
using uBinding.Changables;
using uBinding.Converters;
using uBinding.Core;

namespace uBinding.Descriptions
{
    public class BindingDescription<TSourceValue, TTargetValue>
    {
        private readonly INotifyPropertyChanged _source;
        private readonly string _sourcePropertyName;
        private readonly object _target;
        private readonly string _targetPropertyName;
        private IConverter<TSourceValue, TTargetValue> _converter;
        private BindingMode _mode;
        private object _raiser;

        public BindingDescription(INotifyPropertyChanged source, string sourcePropertyName, object target,
            string targetPropertyName)
        {
            _source = source;
            _sourcePropertyName = sourcePropertyName;
            _target = target;
            _targetPropertyName = targetPropertyName;
        }

        public BindingSet Set { private get; set; }

        public IBinder Apply()
        {
            var source = new BindableSource<TSourceValue>(_source, _sourcePropertyName, _mode);

            var sourceValueType = typeof (TSourceValue);
            var targetValueType = typeof (TTargetValue);

            IBinder binder;

            if (sourceValueType == targetValueType)
            {
                if (_converter != null)
                    throw new InvalidOperationException("Converter is redundant if types are the same.");

                var target = new BindableTarget<TSourceValue>(_target, _targetPropertyName, _raiser, _mode);
                binder = new Binder<TSourceValue>(source, target, _mode);
            }
            else
            {
                if (_converter == null)
                {
                    var message = string.Format("Converter is not specified for '{0}:{1}:{2}' and '{3}:{4}:{5}'.",
                        _source.GetType().Name, _sourcePropertyName, sourceValueType.Name,
                        _target.GetType().Name, _targetPropertyName, targetValueType.Name);
                    throw new InvalidOperationException(message);
                }

                var target = new BindableTarget<TTargetValue>(_target, _targetPropertyName, _raiser, _mode);
                binder = new Binder<TSourceValue, TTargetValue>(source, target, _converter, _mode);
            }

            if (Set == null) throw new InvalidOperationException("Binding set is not set");
                
            Set.Add(binder);
            binder.Start();
            return binder;
        }

        public BindingDescription<TSourceValue, TTargetValue> With(Action<IInvokable> action)
        {
            _raiser = new EventPasser(action);
            return this;
        }

        public BindingDescription<TSourceValue, TTargetValue> With(IConverter<TSourceValue, TTargetValue> converter)
        {
            _converter = converter;
            return this;
        }

        public BindingDescription<TSourceValue, TTargetValue> OneWay()
        {
            _mode = BindingMode.OneWay;
            return this;
        }
    }
}