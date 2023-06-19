namespace CarParkingSystem.Contracts
{
    public interface IUserService
    {
        Task ChangeEmailAsync(string userId, string email);

        Task ChangeCarInfoAsync(string userId, string carMake, string carModel, string carNumber);
    }
}
