using System;

namespace uBinding.Converters
{
    public class GenericConverter<TSourceValue, TTargetValue> : IConverter<TSourceValue, TTargetValue>
    {
        private readonly Func<TTargetValue, TSourceValue> _convert;
        private readonly Func<TSourceValue, TTargetValue> _convertBack;

        public GenericConverter(Func<TTargetValue, TSourceValue> convert, Func<TSourceValue, TTargetValue> convertBack)
        {
            _convert = convert;
            _convertBack = convertBack;
        }

        public TSourceValue Convert(TTargetValue value)
        {
            return _convert(value);
        }

        public TTargetValue ConvertBack(TSourceValue value)
        {
            return _convertBack(value);
        }
    }
}