using System;
using System.Globalization;
using System.Windows.Data;

namespace NextUI.Converters
{
    public class EqualsValueConverter : IValueConverter
    {
        public object TargetValue { get; set; } = null;

        public object EqualValue { get; set; } = Binding.DoNothing;

        public object UnequalValue { get; set; } = Binding.DoNothing;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                if (TargetValue == null)
                {
                    return EqualValue;
                }
                return UnequalValue;
            }
            if (value.Equals(TargetValue))
            {
                return EqualValue;
            }
            return UnequalValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value?.Equals(EqualValue) == true)
            {
                return EqualValue;
            }
            if (value?.Equals(UnequalValue) == true)
            {
                return UnequalValue;
            }
            return Binding.DoNothing;
        }
    }
}
