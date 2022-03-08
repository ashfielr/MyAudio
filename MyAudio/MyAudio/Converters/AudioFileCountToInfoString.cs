namespace MyAudio.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using Xamarin.Forms;

    /// <summary>
    /// Converts an integer number of audio file into an info string used when displaying playlists.
    /// </summary>
    internal class AudioFileCountToInfoString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convertValue = (int)value;
            return $"{convertValue} Audio File(s)";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
