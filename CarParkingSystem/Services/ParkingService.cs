using CarParkingSystem.Contracts;
using CarParkingSystem.Data;
using CarParkingSystem.Data.Models;
using CarParkingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace CarParkingSystem.Services
{
    public class ParkingService : IParkingService
    {
        private readonly ApplicationDbContext context;

        public ParkingService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddLocationAsync(ParkingLotViewModel model)
        {
            var parkingLot = new ParkingLot()
            {
                Name = model.Name,
                Capacity = model.Capacity,
                AvailableSpaces = model.Capacity,
                HourlyRate = model.HourlyRate,
                ImageURL = model.ImageUrl,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Address=model.Address,
                OpeningHour = model.OpeningHour,
                ClosingHour = model.ClosingHour,
                IsNonStop = model.IsNonStop
            };
            if (context.ParkingLots.Any(p => p.Name == model.Name))
            {
                throw new ArgumentException("Parking lot with this name already exists!");
            }
            await context.ParkingLots.AddAsync(parkingLot);
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<ParkingLotViewModel>> LoadParkingLotsAsync()
        {
            var parkingLots = await context.ParkingLots.ToListAsync();
            return parkingLots.Select(p => new ParkingLotViewModel()
            {
                Name= p.Name,
                Capacity = p.Capacity,
                HourlyRate=p.HourlyRate,
                ImageUrl=p.ImageURL,
                Latitude=p.Latitude,
                Longitude=p.Longitude,
                Address=p.Address,
                AvailableSpaces=p.AvailableSpaces,
                OpeningHour=p.OpeningHour,
                ClosingHour=p.ClosingHour,
                IsNonStop=p.IsNonStop
                
            }).ToList();
        }
    }
}
