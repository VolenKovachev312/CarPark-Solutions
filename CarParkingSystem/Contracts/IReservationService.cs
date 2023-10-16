using CarParkingSystem.Models;

namespace CarParkingSystem.Contracts
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationViewModel>> GetUserReservationsByIdAsync(string userId);

        Task CancelReservationAsync(string reservationId);

        Task<IEnumerable<ReservationViewModel>> GetReservationsByParkingIdAsync(string parkingId);

        Task CreateReservationAsync(ReserveViewModel model);

    }
}
