using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using ELTE.TravelAgency.Data;

namespace ELTE.TravelAgency.Admin.ViewModel
{
    /// <summary>
    /// Tengerpart típus átalakító típusa.
    /// </summary>
    public class ShoreTypeConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            // ellenőrizzük az értéket
            if (value == null || !(value is ShoreType))
                return Binding.DoNothing; // ha nem megfelelő, nem csinálunk semmit

            // ellenőrizzük a paramétert
            if (parameter == null || !(parameter is IEnumerable<String>))
                return Binding.DoNothing;

            List<String> shoreNames = (parameter as IEnumerable<String>).ToList();
            Int32 index = (Int32)value;

            if (index < 0 || index >= shoreNames.Count)
                return Binding.DoNothing;

            return shoreNames[index];
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            // ellenőrizzük az értéket
            if (value == null || !(value is String))
                return DependencyProperty.UnsetValue; // ha nem megfelelő, nem csinálunk semmit

            // ellenőrizzük a paramétert
            if (parameter == null || !(parameter is IEnumerable<String>))
                return Binding.DoNothing;

            List<String> shoreNames = (parameter as IEnumerable<String>).ToList();
            String name = (String)value;

            // megkeressük a nevet
            if (!shoreNames.Contains(name))
                return DependencyProperty.UnsetValue;

            return (ShoreType)shoreNames.IndexOf(name);
        }
    }
}
