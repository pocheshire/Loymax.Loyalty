using System;
using MvvmCross.Converters;

namespace Loyalty.Droid.Converters
{
    public class ErrorFromBooleanValueConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value ? string.Empty : " ";
        }
    }
}
