using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Diagnostics;

namespace EasySaveGUI
{

    public class ColorState : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            
            if ((string)value == "Paused")
            {
                return "Orange";
            }
            if ((string)value == "Ended" || (string)value == "Stopped")
            {
                return "Diff";
            }

            return "Green";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
