using CarParkingSystem.Contracts;
using CarParkingSystem.Data;
using CarParkingSystem.Data.Models;
using CarParkingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarParkingSystem.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext context;
        public ReservationService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task CancelReservationAsync(string reservationId)
        {
            var reservation = await context.Reservations.FirstOrDefaultAsync(r => r.Id.ToString() == reservationId);
            reservation.isCancelled = true;
            await context.SaveChangesAsync();
        }
        public async Task CreateReservationAsync(ReserveViewModel model)
        {
            var parkingLotViewModel = model.ParkingLotViewModel;
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);
            if (user == null && context.Users.Any(u => u.Email == model.Email))
            {
                throw new ArgumentException("User with this email already exists!");
            }
            if (user == null && context.Users.Any(u => u.PhoneNumber == model.PhoneNumber))
            {
                throw new ArgumentException("User with this phone number already exists!");
            }
            var reservation = new Reservation()
            {
                UserId = model.UserId,
                ParkingLotId = parkingLotViewModel.Id,
                Email = model.Email,
                CheckInTime = model.CheckInHour,
                CheckOutTime = model.CheckOutHour,
                Price = model.Price,
                FullName = $"{model.FirstName} {model.LastName}",
                PhoneNumber = model.PhoneNumber,
                LicensePlateNumber=model.LicensePlateNumber
            };
            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ReservationViewModel>> GetReservationsByParkingIdAsync(string parkingId)
        {
            var reservations = await context.Reservations.Include(r => r.ParkingLot).Include(r=>r.User).Where(r => r.ParkingLotId.ToString()==parkingId).ToListAsync();
            return reservations.Select(r => new ReservationViewModel
            {
                Address = r.ParkingLot.Address,
                CheckInTime = r.CheckInTime,
                CheckOutTime = r.CheckOutTime,
                ParkingName = r.ParkingLot.Name,
                Price = r.Price,
                ImageUrl = r.ParkingLot.ImageUrl,
                IsCancelled = r.isCancelled,
                ReservationId = r.Id,
                UserName=r.FullName,
                Email=r.Email,
                PhoneNumber=r.PhoneNumber,
                LicensePlateNumber=r.LicensePlateNumber
            }).ToList();
        }

        public async Task<IEnumerable<ReservationViewModel>> GetUserReservationsByIdAsync(string userId)
        {
            var reservations = await context.Reservations.Include(r => r.ParkingLot).Where(r => r.UserId.ToString() == userId).ToListAsync();
            return reservations.Select(r => new ReservationViewModel
            {
                Address=r.ParkingLot.Address,
                CheckInTime = r.CheckInTime,
                CheckOutTime = r.CheckOutTime,
                ParkingName=r.ParkingLot.Name,
                Price=r.Price,
                ImageUrl=r.ParkingLot.ImageUrl,
                IsCancelled=r.isCancelled,
                ReservationId=r.Id,
                LicensePlateNumber=r.LicensePlateNumber
            }).ToList();
        }
        
    }
}
