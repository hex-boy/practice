#region Copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Siemens AG" file="BoolToVisibilityConverter.cs">
//   Copyright (C) Siemens AG 2018-2018. All rights reserved. Confidential.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion


#region Used namespaces

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#endregion


namespace AvalonDockMVVM.Converter
{
    public class BoolToVisibilityConverter : IValueConverter
    {

        public BoolToVisibilityConverter()
        {
            FalseValueVisibility = Visibility.Hidden;
        }


        public Visibility FalseValueVisibility { get; set; }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                throw new ArgumentOutOfRangeException(nameof(value));

            var boolValue = (bool) value;

            if (boolValue)
                return Visibility.Visible;

            return FalseValueVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility))
                throw new ArgumentOutOfRangeException(nameof(value));

            var visibility = (Visibility) value;

            return visibility == Visibility.Visible;
        }

    }
}