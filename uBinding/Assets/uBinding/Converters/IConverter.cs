namespace uBinding.Converters
{
    public interface IConverter<TSourceValue, TTargetValue>
    {
        TSourceValue Convert(TTargetValue value);
        TTargetValue ConvertBack(TSourceValue value);
    }
}