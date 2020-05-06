using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Linq;
using System.Windows;
using ELTE.TravelAgency.Data;

namespace ELTE.TravelAgency.Admin.ViewModel
{
    /// <summary>
    /// Jellemző megjelenítés átalakító típusa.
    /// </summary>
    public class FeatureDisplayConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            // ellenőrizzük az értéket
            if (value == null || !(value is IList<FeatureDTO>))
                return Binding.DoNothing; // ha nem megfelelő, nem csinálunk semmit

            // ellenőrizzük a paramétert
            if (parameter == null || !(parameter is IList<String>))
                return Binding.DoNothing;

            IList<String> featureNames = parameter as IList<String>;
            IList<FeatureDTO> features = value as IList<FeatureDTO>;
            String result = String.Empty;

            for (Int32 featureIndex = 0; featureIndex < features.Count && featureIndex < featureNames.Count; featureIndex++)
                if (features[featureIndex].IsAvailable)
                    result += featureNames[features[featureIndex].Id] + ", "; // a jellemzőket összerakjuk

            if (result.Length > 0)
                return result.Substring(0, result.Length - 2); // a megfelelő szöveget visszaadjuk
            else
                return String.Empty;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture)
        {
            // a visszaalakítással nem törődünk
            return DependencyProperty.UnsetValue;
        }
    }
}
