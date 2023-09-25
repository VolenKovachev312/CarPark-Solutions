using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarParkingSystem.Data.Converters
{
    public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
    {
        public TimeOnlyConverter() : base(timeOnly =>
                timeOnly.ToTimeSpan(),
            timeSpan => TimeOnly.FromTimeSpan(timeSpan))
        { }
    }
}
