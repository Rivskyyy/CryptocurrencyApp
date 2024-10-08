﻿using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CryptocurrencyApp.Converters
{
    public class BoolToRefreshTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isLoading = (bool)value;
            return isLoading ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
