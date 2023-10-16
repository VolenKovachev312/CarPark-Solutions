using CarParkingSystem.Contracts;
using CarParkingSystem.Data;
using CarParkingSystem.Data.Models;
using CarParkingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
                ImageUrl = model.ImageUrl,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Address=model.Address,
                OpeningHour = model.OpeningHour,
                ClosingHour = model.ClosingHour,
                IsNonStop = model.IsNonStop,
            };
            if (context.ParkingLots.Any(p => p.Name == model.Name))
            {
                throw new ArgumentException("Parking lot with this name already exists!");
            }
            await context.ParkingLots.AddAsync(parkingLot);
            await context.SaveChangesAsync();

        }

       

        public async Task EditParkingLotAsync(string nameToEdit, ParkingLotViewModel model)
        {
            var parkingLot = await context.ParkingLots.FirstOrDefaultAsync(p=>p.Name.ToLower() == nameToEdit.ToLower());
            if (parkingLot == null)
            {
                throw new ArgumentException("Parking lot with this name does not exist!");
            }
            if(await context.ParkingLots.AnyAsync(p=>p.Name==model.Name&&model.Name!=nameToEdit))
            {
                throw new ArgumentException("Parking lot with this name already exists!");
            }
            if (await context.ParkingLots.AnyAsync(p => p.Address == model.Address&&p.Name!=nameToEdit))
            {
                throw new ArgumentException("Parking lot with this address already exists!");
            }
            parkingLot.Name = model.Name;
            parkingLot.Address = model.Address;
            parkingLot.Latitude = model.Latitude;
            parkingLot.Longitude = model.Longitude;
            parkingLot.Capacity = model.Capacity;
            parkingLot.OpeningHour = model.OpeningHour;
            parkingLot.ClosingHour = model.ClosingHour;
            parkingLot.IsNonStop = model.IsNonStop;
            parkingLot.ImageUrl = model.ImageUrl;
            parkingLot.IsDeleted = model.IsDeleted;
            parkingLot.HourlyRate = model.HourlyRate;
            
        }

        public async Task<ParkingLotViewModel> GetParkingLotAsync(string nameToEdit)
        {
            var parkingLot = await context.ParkingLots.FirstOrDefaultAsync(p => p.Name.ToLower() == nameToEdit.ToLower());
            if (parkingLot == null)
            {
                throw new ArgumentException($"Parking Lot with name {nameToEdit} does not exist.");
            }
            if (parkingLot.IsDeleted)
            {
                throw new ArgumentException();
            }
            var parkingLotViewModel = new ParkingLotViewModel()
            {
                Id = parkingLot.Id,
                Address = parkingLot.Address,
                AvailableSpaces = parkingLot.AvailableSpaces,
                Capacity = parkingLot.Capacity,
                ClosingHour = parkingLot.ClosingHour,
                HourlyRate = parkingLot.HourlyRate,
                ImageUrl = parkingLot.ImageUrl,
                IsNonStop = parkingLot.IsNonStop,
                Latitude = parkingLot.Latitude,
                Longitude = parkingLot.Longitude,
                Name = parkingLot.Name,
                OpeningHour = parkingLot.OpeningHour,
                IsDeleted = parkingLot.IsDeleted
            };
            return parkingLotViewModel;
        }

        public async Task<IEnumerable<ParkingLotViewModel>> LoadParkingLotsAsync()
        {
            var parkingLots = await context.ParkingLots.ToListAsync();
            return parkingLots.Select(p => new ParkingLotViewModel()
            {
                Id=p.Id,
                Name= p.Name,
                Capacity = p.Capacity,
                HourlyRate=p.HourlyRate,
                ImageUrl=p.ImageUrl,
                Latitude=p.Latitude,
                Longitude=p.Longitude,
                Address=p.Address,
                AvailableSpaces=p.AvailableSpaces,
                OpeningHour=p.OpeningHour,
                ClosingHour=p.ClosingHour,
                IsNonStop=p.IsNonStop,
                IsDeleted=p.IsDeleted
                
            }).ToList();
        }
    }
}
