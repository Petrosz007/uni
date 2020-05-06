using ELTE.TravelAgency.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ELTE.TravelAgency.Admin.ViewModel
{
    /// <summary>
    /// Város átalakító.
    /// </summary>
    public class CityConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            // ellenőrizzük az értéket
            if (value == null || !(value is Int32))
                return Binding.DoNothing; // ha nem megfelelő, nem csinálunk semmit

            Int32 id = (Int32)value;
            CityDTO city = (parameter as IEnumerable<CityDTO>).FirstOrDefault(c => c.Id == id);

            if (city == null)
                return Binding.DoNothing;

            return city;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            // ellenőrizzük az értéket
            if (value == null || !(value is CityDTO))
                return DependencyProperty.UnsetValue; // ha nem megfelelő, nem csinálunk semmit

            return ((CityDTO)value).Id;
        }
    }
}
