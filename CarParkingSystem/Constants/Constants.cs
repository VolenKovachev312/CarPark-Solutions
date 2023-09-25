using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Constants
{
    public static class Constants
    {
        public static class ParkingLot
        {
            public const int CityLengthMax = 85;
            public const int CityLengthMin = 3;

            public const int CountryLengthMax = 56;
            public const int CountryLengthMin = 4;

            public const int StateLengthMax = 52;
            public const int StateLengthMin = 4;

            //Added +2 and +4 for commas and blank spaces
            public const int LocationLengthMin = CountryLengthMin + CityLengthMin + 2;
            public const int LocationLengthMax = CountryLengthMax + StateLengthMax + CityLengthMax + 4;

            public const int CapacityMax = int.MaxValue;
            public const int CapacityMin = 1;

            public const double HourlyRateMax = double.MaxValue;
            public const double HourlyRateMin = 0;

            public const double LatitudeMax = 90;
            public const double LatitudeMin = -90;

            public const double LongitudeMax = 180;
            public const double LongitudeMin = -180;
        }
    }
}
