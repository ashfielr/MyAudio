namespace MyAudio.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using Xamarin.Forms;

    /// <summary>
    /// Converter class that converts milliseconds to a time string format - MM:SS.
    /// </summary>
    internal class MillisecondsToTimeFormatStr : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convertedVal = (int)value;
            int mins = convertedVal / 1000 / 60;
            int sec = (convertedVal - (mins * 1000 * 60)) / 1000;

            string minsStr;
            string secStr;

            if (mins < 10)
            {
                minsStr = $"0{mins}";
            }
            else
            {
                minsStr = mins.ToString();
            }

            if (sec < 10)
            {
                secStr = $"0{sec}";
            }
            else
            {
                secStr = sec.ToString();
            }

            return $"{minsStr}:{secStr}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
