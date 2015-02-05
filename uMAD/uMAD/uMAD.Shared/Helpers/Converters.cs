using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace uMAD.Helpers
{
    public class ScheduleDateConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string returned = value.ToString();
            if (value is DateTime)
            {
                var date = (DateTime)value;
                returned = date.ToString("hh:mmt ddd");
            }


            return returned;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    public class CapitalConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string returned = value.ToString();
            return returned.ToUpper(); ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class RelativeDateTimeConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        private const int Minute = 60;
        private const int Hour = Minute * 60;
        private const int Day = Hour * 24;
        private const int Year = Day * 365;

        private readonly Dictionary<long, Func<TimeSpan, string>> thresholds = new Dictionary<long, Func<TimeSpan, string>>
        {
            {2, t => "1s"},
            {Minute,  t => String.Format("{0}s", (int)t.TotalSeconds)},
            {Minute * 2,  t => "1m"},
            {Hour,  t => String.Format("{0}m", (int)t.TotalMinutes)},
            {Hour * 2,  t => "1h"},
            {Day,  t => String.Format("{0}h", (int)t.TotalHours)},
            {Day * 2,  t => "1d"},
            {Day * 30,  t => String.Format("{0}d", (int)t.TotalDays)},
            {Day * 60,  t => "1mon"},
            {Year,  t => String.Format("{0}mon", (int)t.TotalDays / 30)},
            {Year * 2,  t => "1y"},
            {Int64.MaxValue,  t => String.Format("{0}y", (int)t.TotalDays / 365)}
        };

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dateTime = (DateTime)value;
            var difference = DateTime.UtcNow - dateTime.ToUniversalTime();
            if (difference.TotalSeconds > Day)
                return dateTime.ToString("MMM d");
            return thresholds.First(t => difference.TotalSeconds < t.Key).Value(difference);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
