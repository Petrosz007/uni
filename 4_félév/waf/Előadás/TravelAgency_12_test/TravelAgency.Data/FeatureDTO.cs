using System;
using System.Collections.Generic;

namespace ELTE.TravelAgency.Data
{
    /// <summary>
    /// Jellemző típusa.
    /// </summary>
    public class FeatureDTO
    {
        /// <summary>
        /// Jellemző azonosítója.
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// Jellemző elérhetősége.
        /// </summary>
        public Boolean IsAvailable { get; set; }

        /// <summary>
        /// Egyenlőségvizsgálat.
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return (obj is FeatureDTO dto) &&
                   Id == dto.Id &&
                   IsAvailable == dto.IsAvailable;
        }

        /// <summary>
        /// Hash-kulcs generálás.
        /// </summary>
        public override int GetHashCode()
        {
            return Id;
        }

        /// <summary>
        /// Jellemzők átalakítása.
        /// </summary>
        /// <param name="features">A jellemzőket reprezentáló szám.</param>
        /// <returns>A jellemzők tömbje.</returns>
        public static FeatureDTO[] Convert(Feature features)
        {
            List<FeatureDTO> result = new List<FeatureDTO>();

            Int32 featureId = 0;
            foreach (Feature feature in Enum.GetValues(typeof(Feature)))
            {
                if (feature > 0)
                {
                    result.Add(new FeatureDTO
                    {
                        Id = featureId++,
                        IsAvailable = features.HasFlag(feature)
                    });
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Jellemzők átalakítása.
        /// </summary>
        /// <param name="features">A jellemzőket tömbje.</param>
        /// <returns>A jellemzőket reprezentáló szám.</returns>
        public static Feature Convert(FeatureDTO[] features)
        {
            if (features == null || features.Length == 0)
                return Feature.None;

            Feature result = Feature.None;
            foreach (var feature in features)
            {
                if (feature.IsAvailable)
                {
                    result += (1 << feature.Id);
                }
            }

            return result;
        }
    }
}
