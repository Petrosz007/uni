using System;
using System.Windows;
using System.Windows.Data;

namespace ELTE.TravelAgency.Admin.ViewModel
{
    /// <summary>
    /// Tengerpart távolság átalakító.
    /// </summary>
    public class SeaDistanceConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is Int32)) // ellenőrizzük az értéket
                return Binding.DoNothing; // ha nem megfelelő, nem csinálunk semmit

            Int32 distance = (Int32)value;
            if (distance > 1000) // megfelelő utótaggal látjuk el
                return (distance / 1000) + " km";
            else
                return distance + " m";
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is String)) // ellenőrizzük az értéket
                return DependencyProperty.UnsetValue; // ha nem megfelelő, nem tudjuk az értéket beállítani

            try
            {
                String distanceString = value as String;
                Double distance;
                if (distanceString.EndsWith(" km")) // megpróbáljuk konvertálni az értéket
                    distance = Double.Parse(distanceString.Substring(0, distanceString.Length - 3)) * 1000; // figyelembe vesszük, hogy törtet is beírhat 
                else
                    distance = Double.Parse(distanceString.Substring(0, distanceString.Length - 2));

                if (distance < 0) // negatív sem lehet az érték
                    return DependencyProperty.UnsetValue;

                return (Int32)distance;
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
