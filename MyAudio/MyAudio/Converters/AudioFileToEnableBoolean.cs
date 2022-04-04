namespace MyAudio.Converters
{
    using System;
    using System.Globalization;
    using MyAudio.Models;
    using Xamarin.Forms;

    /// <summary>
    /// Converter to be used to enable or disable the access to the currently playing audio file page
    /// (bool return value will be used on the IsEnabled property)
    /// When false is returned the access will be disabled
    /// When true is returned the access will be enabled
    /// </summary>
    public class AudioFileToEnableBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AudioFile convertValue = (AudioFile)value;
            if (convertValue == null)
            {
                return false;
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
