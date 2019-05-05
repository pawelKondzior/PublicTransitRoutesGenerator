// -----------------------------------------------------------------------
//  <copyright file="TimeConverter.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------
namespace Magisterka.Application.Wpf.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Magisterka.Infrastructure.Shared.Structure;

    /// <summary>
    /// The time converter.
    /// </summary>
    [ValueConversion(typeof(Time), typeof(string))]
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            Time time = (Time)value;

            return time.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}