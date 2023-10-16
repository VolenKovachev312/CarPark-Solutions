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
    public class ReservationServiceTest
    {
        private readonly ApplicationDbContext context = DatabaseMock.Instance;
        private readonly ReservationService reservationService;

        public ReservationServiceTest()
        {
            reservationService = new ReservationService(context);
            SeedDatabase();
        }
        [Fact]
        public async Task CancelReservationShouldCancelReservation()
        {
            var reservation = await context.Reservations.FirstOrDefaultAsync();
            var reservationId = reservation.Id.ToString();
            Assert.False(reservation.isCancelled);

            await reservationService.CancelReservationAsync(reservationId);

            Assert.True(reservation.isCancelled);
        }
        
        [Fact]
        public async Task CreateReservationShouldCreateNewReservationRegistered()
        {
            var parkingLotViewModel = new ParkingLotViewModel()
            {
                Name = "Parking 1",
                Address = "Address test parking 1",
                Capacity = 20,
                AvailableSpaces = 20,
                ImageUrl = "imageUrl",
                HourlyRate = 5.50M,
                Latitude = 40,
                Longitude = 20,
                OpeningHour = new TimeOnly(10, 0, 0),
                ClosingHour = new TimeOnly(18, 0, 0),
                IsNonStop = false
            };
            var user = await context.Users.Include(u=>u.Reservations).FirstOrDefaultAsync();
            var reserveViewModel = new ReserveViewModel()
            {
                UserId=user.Id,
                CheckInHour=new DateTime(2023,10,18,11,0,0),
                CheckOutHour=new DateTime(2023,10,18,15,0,0),
                Price=22,
                Email=user.Email,
                FirstName=user.FirstName,
                LastName=user.LastName,
                LicensePlateNumber="EN2323EN",
                PhoneNumber=user.PhoneNumber,
                ParkingLotViewModel=parkingLotViewModel
            };
            Assert.Equal(3, context.Reservations.Count());
            Assert.Equal(2,user.Reservations.Count());

            await reservationService.CreateReservationAsync(reserveViewModel);

            Assert.Equal(4, context.Reservations.Count());
            Assert.Equal(3, user.Reservations.Count());
        }
        [Fact]
        public async Task CreateReservationShouldCreateNewReservationNotRegistered()
        {
            var parkingLotViewModel = new ParkingLotViewModel()
            {
                Name = "Parking 1",
                Address = "Address test parking 1",
                Capacity = 20,
                AvailableSpaces = 20,
                ImageUrl = "imageUrl",
                HourlyRate = 5.50M,
                Latitude = 40,
                Longitude = 20,
                OpeningHour = new TimeOnly(10, 0, 0),
                ClosingHour = new TimeOnly(18, 0, 0),
                IsNonStop = false
            };
            var user = await context.Users.Include(u => u.Reservations).FirstOrDefaultAsync();
            var reserveViewModel = new ReserveViewModel()
            {
                UserId = null,
                CheckInHour = new DateTime(2023, 10, 18, 11, 0, 0),
                CheckOutHour = new DateTime(2023, 10, 18, 15, 0, 0),
                Price = 22,
                Email = "newEmail@gmail.com",
                FirstName = "fNameInput",
                LastName = "lNameInput",
                LicensePlateNumber = "EN2323EN",
                PhoneNumber = "+359221122112",
                ParkingLotViewModel = parkingLotViewModel
            };
            Assert.Equal(3, context.Reservations.Count());

            await reservationService.CreateReservationAsync(reserveViewModel);

            Assert.Equal(4, context.Reservations.Count());
        }
        [Fact]
        public async Task CreateReservationShouldCreateNewReservationShouldThrowWhenEmailExists()
        {
            var parkingLotViewModel = new ParkingLotViewModel()
            {
                Name = "Parking 1",
                Address = "Address test parking 1",
                Capacity = 20,
                AvailableSpaces = 20,
                ImageUrl = "imageUrl",
                HourlyRate = 5.50M,
                Latitude = 40,
                Longitude = 20,
                OpeningHour = new TimeOnly(10, 0, 0),
                ClosingHour = new TimeOnly(18, 0, 0),
                IsNonStop = false
            };
            var user = await context.Users.Include(u => u.Reservations).FirstOrDefaultAsync();
            var reserveViewModel = new ReserveViewModel()
            {
                UserId = null,
                CheckInHour = new DateTime(2023, 10, 18, 11, 0, 0),
                CheckOutHour = new DateTime(2023, 10, 18, 15, 0, 0),
                Price = 22,
                Email = "testuser1@gmail.com",
                FirstName = "fNameInput",
                LastName = "lNameInput",
                LicensePlateNumber = "EN2323EN",
                PhoneNumber = "+359221122112",
                ParkingLotViewModel = parkingLotViewModel
            };

            await Assert.ThrowsAsync<ArgumentException>(async()=> await reservationService.CreateReservationAsync(reserveViewModel));

        }
        [Fact]
        public async Task CreateReservationShouldCreateNewReservationShouldThrowWhenPhoneNumberInUse()
        {
            var parkingLotViewModel = new ParkingLotViewModel()
            {
                Name = "Parking 1",
                Address = "Address test parking 1",
                Capacity = 20,
                AvailableSpaces = 20,
                ImageUrl = "imageUrl",
                HourlyRate = 5.50M,
                Latitude = 40,
                Longitude = 20,
                OpeningHour = new TimeOnly(10, 0, 0),
                ClosingHour = new TimeOnly(18, 0, 0),
                IsNonStop = false
            };
            var user = await context.Users.Include(u => u.Reservations).FirstOrDefaultAsync();
            var reserveViewModel = new ReserveViewModel()
            {
                UserId = null,
                CheckInHour = new DateTime(2023, 10, 18, 11, 0, 0),
                CheckOutHour = new DateTime(2023, 10, 18, 15, 0, 0),
                Price = 22,
                Email = "testuser233@gmail.com",
                FirstName = "fNameInput",
                LastName = "lNameInput",
                LicensePlateNumber = "EN2323EN",
                PhoneNumber = "+359111111111",
                ParkingLotViewModel = parkingLotViewModel
            };

            await Assert.ThrowsAsync<ArgumentException>(async () => await reservationService.CreateReservationAsync(reserveViewModel));

        }

        [Fact]
        public async Task GetReservationsByParkingIdShouldReturnAllReservations()
        {
            var parkingLot = await context.ParkingLots.Include(p=>p.Reservations).FirstOrDefaultAsync();
            var parkingLotId = parkingLot.Id.ToString();

            var reservations=await reservationService.GetReservationsByParkingIdAsync(parkingLotId);

            Assert.Equal(2, parkingLot.Reservations.Count());
            Assert.Equal(2, reservations.Count());
            Assert.IsType<List<ReservationViewModel>>(reservations);

        }
        [Fact]
        public async Task GetUserReservationsByIdShouldReturnReservations()
        {
            var user = await context.Users.FirstOrDefaultAsync();
            var userId = user.Id.ToString();

            var reservations = await reservationService.GetUserReservationsByIdAsync(userId);

            Assert.Equal(2, reservations.Count());
            Assert.IsType<List<ReservationViewModel>>(reservations);

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
                    RowVersion=Array.Empty<byte>()
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
                    RowVersion=Array.Empty<byte>()
                },
            };
            context.ParkingLots.AddRange(parkingLots);
            var users = new List<User>()
            {
                new User
                {
                    Email="testuser1@gmail.com",
                    UserName="testuser1@gmail.com",
                    FirstName="Test1",
                    LastName="Test1",
                    PhoneNumber="+359111111111",
                },
                new User
                {
                    Email="testuser2@gmail.com",
                    UserName="testuser2@gmail.com",
                    FirstName="Test2",
                    LastName="Test2",
                    PhoneNumber="+359222222222",
                },
                new User
                {
                    Email="testuser3@gmail.com",
                    UserName="testuser3@gmail.com",
                    FirstName="Test3",
                    LastName="Test3",
                    PhoneNumber="+359333333333",
                },
            };
            context.Users.AddRange(users);
            var reservations = new List<Reservation>()
            {
                new Reservation()
                {
                    ParkingLot=parkingLots[0],
                    User=users[0],
                    Email="testuser1@gmail.com",
                    FullName="Test1 Test1",
                    PhoneNumber="+359111111111",
                    LicensePlateNumber="TE1111ST",
                    Price=22,
                    CheckInTime=new DateTime(2023,10,12,10,0,0),
                    CheckOutTime=new DateTime(2023,10,12,16,0,0),
                },
                new Reservation()
                {
                    ParkingLot=parkingLots[0],
                    User=users[1],
                    Email="testuser2@gmail.com",
                    FullName="Test2 Test2",
                    PhoneNumber="+359222222222",
                    LicensePlateNumber="TE2222ST",
                    Price=5.50M,
                    CheckInTime=new DateTime(2023,10,12,15,0,0),
                    CheckOutTime=new DateTime(2023,10,12,16,0,0),
                },
                new Reservation()
                {
                    ParkingLot=parkingLots[1],
                    User=users[0],
                    Email="testuser1@gmail.com",
                    FullName="Test1 Test1",
                    PhoneNumber="+359111111111",
                    LicensePlateNumber="TE1111ST",
                    Price=1000,
                    CheckInTime=new DateTime(2023,10,12,10,0,0),
                    CheckOutTime=new DateTime(2023,10,22,10,0,0),
                },
            };
            context.Reservations.AddRange(reservations);
            context.SaveChanges();

        }
    }
}
