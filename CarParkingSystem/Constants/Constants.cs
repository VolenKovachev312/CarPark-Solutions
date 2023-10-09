using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Constants
{
    public static class Constants
    {
        public static class ParkingLot
        {

            //Added +2 and +4 for commas and blank spaces
            public const int LocationLengthMin = 2;
            public const int LocationLengthMax = 150;

            public const int CapacityMax = int.MaxValue;
            public const int CapacityMin = 1;

            public const double HourlyRateMax = double.MaxValue;
            public const double HourlyRateMin = 0;

            public const double LatitudeMax = 90;
            public const double LatitudeMin = -90;

            public const double LongitudeMax = 180;
            public const double LongitudeMin = -180;

        }
        
        public static class User
        {
            public const int FirstNameLengthMax = 20;
            public const int FirstNameLengthMin = 2;

            public const int LastNameLengthMax = 20;
            public const int LastNameLengthMin = 2;
        }

        public static class ErrorMessage
        {
            public const string RequiredErrorMessage = "This input field should not be empty!";
        }
    }
}
