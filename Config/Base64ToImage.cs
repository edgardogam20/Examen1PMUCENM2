using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Examen1PMUCENM2.Converters
{
    public class Base64ToImage : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string base64 && !string.IsNullOrEmpty(base64))
            {
                try
                {
                    byte[] imageBytes = System.Convert.FromBase64String(base64);
                    return ImageSource.FromStream(() => new MemoryStream(imageBytes));
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}