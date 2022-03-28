namespace MyAudio.Converters
{
    using System;
    using System.Globalization;
    using MyAudio.Models;
    using Xamarin.Forms;

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
