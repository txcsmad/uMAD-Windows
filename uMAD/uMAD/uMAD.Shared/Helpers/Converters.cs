using System;
using System.Collections.Generic;
using System.Text;

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
}
