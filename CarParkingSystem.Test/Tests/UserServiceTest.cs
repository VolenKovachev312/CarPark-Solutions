using CarParkingSystem.Data;
using CarParkingSystem.Data.Models;
using CarParkingSystem.Models;
using CarParkingSystem.Services;
using CarParkingSystem.Test.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkingSystem.Test.Tests
{
    public class UserServiceTest
    {
        private readonly ApplicationDbContext context = DatabaseMock.Instance;
        private readonly UserService userService;

        public UserServiceTest()
        {
            userService = new UserService(context);
            SeedDatabase();
        }

        [Fact]
        public async Task ChangeCarInfoShouldChangeLicensePlate()
        {
            var user = await context.Users.FirstOrDefaultAsync();
            var licesnePlateNumber = "ED2431NA";
            Assert.False(user.LicensePlateNumber==licesnePlateNumber);

            await userService.ChangeCarInfoAsync(user.Id.ToString(), licesnePlateNumber);

            Assert.True(user.LicensePlateNumber == licesnePlateNumber);
        }

        [Fact]
        public async Task ChangeCarInfoShouldThrowWhenLicenseEmpty()
        {
            var user = await context.Users.FirstOrDefaultAsync();
            var licesnePlateNumber = "";

            await Assert.ThrowsAsync<ArgumentException>(async ()=>await userService.ChangeCarInfoAsync(user.Id.ToString(), licesnePlateNumber));
        }
        
        [Fact]
        public async Task ChangeEmailShouldChangeEmail()
        {
            var user=await context.Users.FirstOrDefaultAsync();
            var newEmail = "changedEmail@gmail.com";
            Assert.False(user.Email ==  newEmail);

            await userService.ChangeEmailAsync(user.Id.ToString(), newEmail);

            Assert.True(user.Email == newEmail);
            Assert.True(user.UserName == newEmail);
            Assert.True(user.NormalizedEmail== newEmail.ToUpper());
            Assert.True(user.NormalizedUserName == newEmail.ToUpper());
        }

        [Fact]
        public async Task ChangeEmailShouldThrowWhenEmailExists()
        {
            var user = await context.Users.FirstOrDefaultAsync();
            var comparisonUser= await context.Users.LastOrDefaultAsync();
            var newEmail = comparisonUser.Email;

            await Assert.ThrowsAsync<ArgumentException>(async () => await userService.ChangeEmailAsync(user.Id.ToString(), newEmail));

        }

        [Fact]
        public async Task ChangeEmailShouldThrowWhenEmailIsSame()
        {
            var user = await context.Users.FirstOrDefaultAsync();
            var newEmail = user.Email;

            await Assert.ThrowsAsync<ArgumentException>(async () => await userService.ChangeEmailAsync(user.Id.ToString(), newEmail));

        }

        [Fact]
        public async Task ChangeEmailShouldThrowWhenEmailIsEmpty()
        {
            var user = await context.Users.FirstOrDefaultAsync();
            var newEmail = "";

            await Assert.ThrowsAsync<ArgumentException>(async () => await userService.ChangeEmailAsync(user.Id.ToString(), newEmail));

        }

        [Fact]
        public async Task GetUserViewModelShouldReturnCorrectViewModel()
        {
            var user=await context.Users.FirstOrDefaultAsync();

            var userViewModel = await userService.GetUserViewModelAsync(user.Id.ToString());

            Assert.NotNull(userViewModel);
            Assert.IsType<UserViewModel>(userViewModel);
            Assert.Equal(user.Id, userViewModel.Id);
        }

        //public async Task<UserViewModel> GetUserViewModelAsync(string userId)
        //{
        //    var user = await context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        //    return new UserViewModel()
        //    {
        //        Id = user.Id,
        //        Email = user.Email,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        LicensePlateNumber = user.LicensePlateNumber,
        //        PhoneNumber = user.PhoneNumber,
        //    };
        //}
        //public async Task<UserViewModel> GetUserReservationsAsync(string searchQuery)
        //{
        //    if (string.IsNullOrEmpty(searchQuery))
        //    {
        //        throw new ArgumentException("Empty input!");
        //    }
        //    searchQuery = searchQuery.ToLower();
        //    var user = await context.Users.Include(u => u.Reservations).ThenInclude(r => r.ParkingLot).FirstOrDefaultAsync(u => u.Email.ToLower() == searchQuery || u.LicensePlateNumber.ToLower() == searchQuery || u.PhoneNumber == searchQuery);
        //    var reservations = user.Reservations.ToList();
        //    var userViewModel = await GetUserViewModelAsync(user.Id.ToString());
        //    userViewModel.Reservations = reservations.Select(r => new ReservationViewModel
        //    {
        //        Address = r.ParkingLot.Address,
        //        CheckInTime = r.CheckInTime,
        //        CheckOutTime = r.CheckOutTime,
        //        ParkingName = r.ParkingLot.Name,
        //        Price = r.Price,
        //        ImageUrl = r.ParkingLot.ImageUrl,
        //        IsCancelled = r.isCancelled,
        //        ReservationId = r.Id,
        //        LicensePlateNumber = r.LicensePlateNumber
        //    }).ToList();
        //    return userViewModel;
        //}

        //public async Task DeleteUserAsync(string userId)
        //{
        //    var user = await context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        //    if (user == null)
        //    {
        //        throw new ArgumentException("Could not delete user!");
        //    }
        //    user.isDeleted = true;
        //    await context.SaveChangesAsync();
        //}
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
