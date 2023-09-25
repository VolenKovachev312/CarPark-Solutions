using CarParkingSystem.Data.Comparers;
using CarParkingSystem.Data.Converters;
using CarParkingSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarParkingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingLot> ParkingLots { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ParkingLot>(builder =>
            {
                builder.Property(x => x.OpeningHour)
                    .HasConversion<TimeOnlyConverter,TimeOnlyComparer>();
                builder.Property(x => x.ClosingHour)
                    .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
            });
            base.OnModelCreating(builder);   
        }
    }
}