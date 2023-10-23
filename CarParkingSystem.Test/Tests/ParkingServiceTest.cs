using CarParkingSystem.Contracts;
using CarParkingSystem.Data;
using CarParkingSystem.Data.Models;
using CarParkingSystem.Models;
using CarParkingSystem.Services;
using CarParkingSystem.Test.Mocks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingSystem.Test.Tests
{
    public class ParkingServiceTest
    {
        private readonly ApplicationDbContext context = DatabaseMock.Instance;
        private readonly ParkingService parkingService;

        public ParkingServiceTest()
        {
            parkingService = new ParkingService(context);
            SeedDatabase();
        }
        
        [Fact]
        public async Task AddLocationShouldThrowWhenNameExists()
        {
            var parkingLotViewModel = new ParkingLotViewModel()
            {
                Name = "Parking 1",
                Capacity = 12,
                AvailableSpaces = 12,
                HourlyRate = 2.5M,
                ImageUrl = "testUrl",
                Latitude = 40,
                Longitude = 25,
                Address = "TestAddress",
                OpeningHour = new TimeOnly(10, 0, 0),
                ClosingHour = new TimeOnly(18, 0, 0),
                IsNonStop = false
            };

            await Assert.ThrowsAsync<ArgumentException>(async()=>await parkingService.AddLocationAsync(parkingLotViewModel));
        }

        [Fact]
        public async Task EditParkingLotShouldEdit()
        {
            var nameToEdit = "Parking 1";
            var parkingLot = await context.ParkingLots.FirstOrDefaultAsync(p => p.Name == nameToEdit);
            var parkingLotViewModel = new ParkingLotViewModel()
            {
                Name = "Parking Test",
                Capacity = 26,
                AvailableSpaces = 26,
                HourlyRate = 10.5M,
                ImageUrl = "testUrl123",
                Latitude = 22,
                Longitude = 21,
                Address = "TestAddressTest",
                OpeningHour = new TimeOnly(23, 0, 0),
                ClosingHour = new TimeOnly(0, 0, 0),
                IsNonStop = false,
                IsDeleted=true
            };

            await parkingService.EditParkingLotAsync(nameToEdit, parkingLotViewModel);

            Assert.Equal(parkingLotViewModel.Name, parkingLot.Name);
            Assert.Equal(parkingLotViewModel.Capacity, parkingLot.Capacity);
            Assert.Equal(parkingLotViewModel.HourlyRate, parkingLot.HourlyRate);
            Assert.Equal(parkingLotViewModel.ImageUrl, parkingLot.ImageUrl);
            Assert.Equal(parkingLotViewModel.Latitude, parkingLot.Latitude);
            Assert.Equal(parkingLotViewModel.Longitude, parkingLot.Longitude);
            Assert.Equal(parkingLotViewModel.Address, parkingLot.Address);
            Assert.Equal(0,parkingLotViewModel.OpeningHour.CompareTo(parkingLot.OpeningHour));
            Assert.Equal(0, parkingLotViewModel.ClosingHour.CompareTo(parkingLot.ClosingHour));
            Assert.Equal(parkingLotViewModel.IsNonStop, parkingLot.IsNonStop);
            Assert.Equal(parkingLotViewModel.IsDeleted, parkingLot.IsDeleted);
        }
        [Fact]
        public async Task EditParkingShouldThrowWhenParkingLotNotFound()
        {
            var nameToEdit = "";
            var parkingLotViewModel = new ParkingLotViewModel()
            {
                Name = "Parking Test",
                Capacity = 26,
                AvailableSpaces = 26,
                HourlyRate = 10.5M,
                ImageUrl = "testUrl123",
                Latitude = 22,
                Longitude = 21,
                Address = "TestAddressTest",
                OpeningHour = new TimeOnly(23, 0, 0),
                ClosingHour = new TimeOnly(0, 0, 0),
                IsNonStop = false,
                IsDeleted = true
            };

            await Assert.ThrowsAsync<ArgumentException>(async () => await parkingService.EditParkingLotAsync(nameToEdit, parkingLotViewModel));

        }
        [Fact]
        public async Task EditParkingShouldThrowWhenParkingLotNameExists()
        {
            var nameToEdit = "Parking 1";
            var parkingLotViewModel = new ParkingLotViewModel()
            {
                Name = "Parking 2 NonStop",
                Capacity = 26,
                AvailableSpaces = 26,
                HourlyRate = 10.5M,
                ImageUrl = "testUrl123",
                Latitude = 22,
                Longitude = 21,
                Address = "TestAddressTest",
                OpeningHour = new TimeOnly(23, 0, 0),
                ClosingHour = new TimeOnly(0, 0, 0),
                IsNonStop = false,
                IsDeleted = true
            };

            await Assert.ThrowsAsync<ArgumentException>(async () => await parkingService.EditParkingLotAsync(nameToEdit, parkingLotViewModel));

        }
        [Fact]
        public async Task EditParkingShouldThrowWhenParkingLotAddressExists()
        {
            var nameToEdit = "Parking 1";
            var parkingLotViewModel = new ParkingLotViewModel()
            {
                Name = "Parking 1 CHANGED",
                Capacity = 26,
                AvailableSpaces = 26,
                HourlyRate = 10.5M,
                ImageUrl = "testUrl123",
                Latitude = 22,
                Longitude = 21,
                Address = "Address test parking 2",
                OpeningHour = new TimeOnly(23, 0, 0),
                ClosingHour = new TimeOnly(0, 0, 0),
                IsNonStop = false,
                IsDeleted = true
            };

            await Assert.ThrowsAsync<ArgumentException>(async () => await parkingService.EditParkingLotAsync(nameToEdit, parkingLotViewModel));

        }

        [Fact]
        public async Task GetParkingLotShouldReturnViewModel()
        {
            var nameToEdit = "Parking 1";

            var parkingLotViewModel = await parkingService.GetParkingLotAsync(nameToEdit);

            Assert.NotNull(parkingLotViewModel);
            Assert.IsType<ParkingLotViewModel>(parkingLotViewModel);
            Assert.False(parkingLotViewModel.IsDeleted);

        }
        [Fact]
        public async Task GetParkingLotShouldThrowWhenNotExist()
        {
            var nameToEdit = "Parking NotExist";

            await Assert.ThrowsAnyAsync<ArgumentException>(async()=>await parkingService.GetParkingLotAsync(nameToEdit));

        }
        [Fact]
        public async Task GetParkingLotShouldThrowWhenIsDeleted()
        {
            var nameToEdit = "Parking 1";
            var parking = await context.ParkingLots.FirstOrDefaultAsync(p => p.Name == nameToEdit);
            parking.IsDeleted=true;
            Assert.True(parking.IsDeleted);

            await Assert.ThrowsAsync<ArgumentException>(async () => await parkingService.GetParkingLotAsync(nameToEdit));
        }

        [Fact]
        public async Task LoadParkingLotsShouldGetAll()
        {
            var parkingLots = await parkingService.LoadParkingLotsAsync();

            Assert.IsType<List<ParkingLotViewModel>>(parkingLots);
            Assert.True(parkingLots.Count() == 2);
        }
  
         private void SeedDatabase()
        {
            var parkingLots = new List<ParkingLot>()
            {
                new ParkingLot()
                {
                    Name="Parking 1",
                    Address="Address test parking 1",
                    Capacity=20,
                    AvailableSpaces=20,
                    ImageUrl="imageUrl",
                    HourlyRate=5.50M,
                    Latitude=40,
                    Longitude=20,
                    OpeningHour=new TimeOnly(10,0,0),
                    ClosingHour=new TimeOnly(18,0,0),
                    IsNonStop=false,
                },
                new ParkingLot()
                {
                    Name="Parking 2 NonStop",
                    Address="Address test parking 2",
                    Capacity=20,
                    AvailableSpaces=20,
                    ImageUrl="imageUrl",
                    HourlyRate=5,
                    Latitude=45,
                    Longitude=25,
                    OpeningHour=new TimeOnly(10,0,0),
                    ClosingHour=new TimeOnly(18,0,0),
                    IsNonStop=true,
                },
            };
            context.ParkingLots.AddRange(parkingLots);
            context.SaveChanges();
        }
    }
}
