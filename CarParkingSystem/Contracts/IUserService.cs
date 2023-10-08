using CarParkingSystem.Models;

namespace CarParkingSystem.Contracts
{
    public interface IUserService
    {
        Task ChangeEmailAsync(string userId, string email);

        Task ChangeCarInfoAsync(string userId, string licensePlateNumber);

        Task<UserViewModel> GetUserViewModelAsync(string userId);

        Task<UserViewModel> GetUserReservationsAsync(string searchQuery);

    }
}
