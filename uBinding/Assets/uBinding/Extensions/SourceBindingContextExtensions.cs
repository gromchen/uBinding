using System;
using JetBrains.Annotations;
using uBinding.Contexts;
using uBinding.Descriptions;
using UnityEngine.UI;

namespace uBinding.Extensions
{
    public static class SourceBindingContextExtensions
    {
        public static BindingDescription<TSourceValue, float> To<TSourceValue>(
            this SourceBindingContext<TSourceValue> context, [NotNull] Slider slider)
        {
            if (slider == null) throw new ArgumentNullException("slider");

            return context.To(() => slider.value)
                .With(invokable => slider.onValueChanged.AddListener(value => invokable.Invoke()));
        }

        public static BindingDescription<TSourceValue, string> To<TSourceValue>(
            this SourceBindingContext<TSourceValue> context, [NotNull] InputField field)
        {
            if (field == null) throw new ArgumentNullException("field");

            return context.To(() => field.text)
                .With(invokable => field.onValueChange.AddListener(value => invokable.Invoke()));
        }

        public static BindingDescription<TSourceValue, string> To<TSourceValue>(
            this SourceBindingContext<TSourceValue> context, [NotNull] Text text)
        {
            if (text == null) throw new ArgumentNullException("text");

            return context.To(() => text.text).OneWay();
        }
    }
}