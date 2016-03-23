using System;
using System.Globalization;
using System.Windows.Data;

namespace ModernDev.PaperUI
{
    public class ShadowDepthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => PaperShadow.ElevationData[(int) (ShadowElevations) value];

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }
}