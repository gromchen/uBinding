using System.Globalization;
using uBinding.Converters;
using uBinding.Descriptions;

namespace uBinding.Extensions
{
    public static class BindingDescriptionExtensions
    {
        public static BindingDescription<float, string> WithDefaultConverter(
           this BindingDescription<float, string> description)
        {
            return description.With(new GenericConverter<float, string>(
                s =>
                {
                    float result;
                    return float.TryParse(s, out result) ? result : 0;
                },
                f => f.ToString(CultureInfo.InvariantCulture)));
        }

        public static BindingDescription<int, float> WithDefaultConverter(
            this BindingDescription<int, float> description)
        {
            return description.With(new GenericConverter<int, float>(f => (int)f, i => (float)i));
        }
    }
}