using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CitizenShipApp.Converters
{
    public class MultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var isCorrectAnswer = (bool)values[0];
            bool isChecked = (bool)values[1];
            string checkedIcon = parameter.ToString();
            if (isCorrectAnswer && isChecked)
            {
                if (checkedIcon == "correct")
                {
                    return Visibility.Visible;
                }
            }
            else if (!isCorrectAnswer && isChecked)
            {
                if (checkedIcon == "inCorrect")
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
