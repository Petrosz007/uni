using ELTE.TravelAgency.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ELTE.TravelAgency.Admin.ViewModel
{
    /// <summary>
    /// Város gyűjtemény átalakító.
    /// </summary>
    public class CityCollectionConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            // itt a városok listájából állítunk elő szöveget
            if (value == null || !(value is IEnumerable<CityDTO>)) // ellenőrizzük az értéket
                return Binding.DoNothing; // ha nem megfelelő, nem csinálunk semmit

            return (value as IEnumerable<CityDTO>).Select(city => city.Name);
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            // a visszalakítással nem törődünk
            return DependencyProperty.UnsetValue;
        }
    }
}
